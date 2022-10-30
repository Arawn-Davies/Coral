﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Coral.Database;
using Coral.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Coral.Services;

public interface IIndexerService
{
    public void ReadDirectory(string directory);
}

public class IndexerService : IIndexerService
{
    private readonly CoralDbContext _context;
    private static readonly string[] AudioFileFormats = { ".flac", ".mp3", ".wav", ".m4a", ".ogg", ".alac" };
    private static readonly string[] ImageFileFormats = { ".jpg", ".png" };
    private static readonly string[] ImageFileNames = { "cover", "artwork", "folder", "front" };

    public IndexerService(CoralDbContext context)
    {
        _context = context;
    }

    private bool ContentDirectoryNeedsRescan(DirectoryInfo contentDirectory)
    {
        try
        {
            var maxValue = _context.Tracks.Max(t => t.DateModified);
            var contentsLastModified = contentDirectory
                .EnumerateFiles("*.*", SearchOption.AllDirectories)
                .Where(f => AudioFileFormats.Contains(Path.GetExtension(f.FullName)))
                .Max(x => x.LastWriteTimeUtc);
            return maxValue < contentsLastModified;
        }
        catch (InvalidOperationException)
        {
            return true;
        }
    }

    public void ReadDirectory(string directory)
    {
        var contentDirectory = new DirectoryInfo(directory);
        if (!contentDirectory.Exists)
        {
            throw new ApplicationException("Content directory does not exist.");
        }

        if (!ContentDirectoryNeedsRescan(contentDirectory)) return;

        var directoryGroups = contentDirectory.EnumerateFiles("*.*", SearchOption.AllDirectories)
            .Where(f => AudioFileFormats.Contains(Path.GetExtension(f.FullName)))
            .GroupBy(f => f.Directory?.Name, f => f);

        // enumerate directories
        foreach (var directoryGroup in directoryGroups)
        {
            var tracksInDirectory = directoryGroup.ToList();

            // we generally shouldn't be introducing side-effects in linq
            // but it's a lot prettier this way ;_;
            var analyzedTracks = tracksInDirectory.Select(x => new ATL.Track(x.FullName)).ToList();
            bool folderIsAlbum = analyzedTracks
                .Where(x => !string.IsNullOrEmpty(x.Album))
                .Select(x => x.Album)
                .Distinct().Count() == 1;

            if (folderIsAlbum)
            {
                IndexAlbum(analyzedTracks);
            }
            else
            {
                IndexSingleFiles(analyzedTracks);
            }
        }

        _context.SaveChanges();
    }

    private void IndexSingleFiles(List<ATL.Track> tracks)
    {
        foreach (var atlTrack in tracks)
        {
            var indexedArtist = GetArtist(atlTrack.Artist);
            var indexedAlbum = GetAlbum(new List<Artist>()
            {
                indexedArtist   
            }, atlTrack);
            var indexedGenre = GetGenre(atlTrack.Genre);
            IndexFile(indexedArtist, indexedAlbum, indexedGenre, atlTrack);
        }
    }

    private void IndexAlbum(List<ATL.Track> tracks)
    {
        // verify that the collection is not empty
        if (!tracks.Any())
        {
            throw new ArgumentException("The track collection cannot be empty.");
        }

        // verify that we in fact have an album
        if (tracks.Select(t => t.Album).Distinct().Count() > 1)
        {
            throw new ArgumentException("The tracks are not from the same album.");
        }

        // get all artists from tracks
        var distinctArtists = tracks.Select(t => t.Artist).Distinct();
        var distinctGenres = tracks.Select(t => t.Genre).Distinct();
        var createdArtists = new List<Artist>();
        var createdGenres = new List<Genre>();

        foreach (var artist in distinctArtists)
        {
            createdArtists.Add(GetArtist(artist));
        }

        foreach (var genre in distinctGenres)
        {
            var indexedGenre = GetGenre(genre);
            if (indexedGenre == null)
            {
                continue;
            }
            createdGenres.Add(indexedGenre);
        }

        // most attributes are going to be the same in an album
        var indexedAlbum = GetAlbum(createdArtists, tracks.First());
        foreach (var trackToIndex in tracks)
        {
            var targetArtist = createdArtists.Single(a => a.Name == trackToIndex.Artist);
            var targetGenre = createdGenres.SingleOrDefault(g => g.Name == trackToIndex.Genre);
            IndexFile(targetArtist, indexedAlbum, targetGenre, trackToIndex);
        }
    }

    private void IndexFile(Artist indexedArtist, Album indexedAlbum, Genre? indexedGenre, ATL.Track atlTrack)
    {
        var indexedTrack = _context.Tracks.FirstOrDefault(t => t.FilePath == atlTrack.Path);
        if (indexedTrack != null)
        {
            return;
        }

        indexedTrack = new Track()
        {
            Album = indexedAlbum,
            Artist = indexedArtist,
            Title = !string.IsNullOrEmpty(atlTrack.Title) ? atlTrack.Title : Path.GetFileName(atlTrack.Path),
            Comment = atlTrack.Comment,
            Genre = indexedGenre,
            DateIndexed = DateTime.UtcNow,
            DateModified = File.GetLastWriteTimeUtc(atlTrack.Path),
            DiscNumber = atlTrack.DiscNumber,
            TrackNumber = atlTrack.TrackNumber,
            DurationInSeconds = atlTrack.Duration,
            FilePath = atlTrack.Path
        };
        _context.Tracks.Add(indexedTrack);
    }

    private Genre? GetGenre(string? genreName)
    {
        if (genreName == null) return null;
        var indexedGenre = _context.Genres.FirstOrDefault(g => g.Name == genreName);
        if (indexedGenre == null)
        {
            indexedGenre = new Genre()
            {
                Name = genreName,
                DateIndexed = DateTime.UtcNow
            };
            _context.Genres.Add(indexedGenre);
        }

        return indexedGenre;
    }

    private Artist GetArtist(string artistName)
    {
        if (string.IsNullOrEmpty(artistName)) artistName = "Unknown Artist";
        var indexedArtist = _context.Artists.FirstOrDefault(a => a.Name == artistName);
        if (indexedArtist == null)
        {
            indexedArtist = new Artist()
            {
                Name = artistName,
                DateIndexed = DateTime.UtcNow
            };
            _context.Artists.Add(indexedArtist);
        }
        return indexedArtist;
    }

    private string? GetAlbumArtwork(ATL.Track atlTrack)
    {
        // get artwork from file parent folder
        var albumDirectory = new DirectoryInfo(atlTrack.Path)
            .Parent;

        var artwork = albumDirectory?.EnumerateFiles("*", SearchOption.TopDirectoryOnly)
            .FirstOrDefault(f => ImageFileFormats.Contains(Path.GetExtension(f.FullName)));

        return artwork?.FullName;
    }

    private Album GetAlbum(List<Artist> artists, ATL.Track atlTrack)
    {
        var albumName = !string.IsNullOrEmpty(atlTrack.Album) ? atlTrack.Album : "Unknown Album";
        var indexedAlbum = _context.Albums
            .Include(x => x.Artists)
            .Where(a => a.Artists.Any(dbArtist => artists.Contains(dbArtist)))
            .FirstOrDefault(a => a.Name == albumName);
        if (indexedAlbum == null)
        {
            indexedAlbum = new Album()
            {
                Artists = artists,
                Name = atlTrack.Album,
                ReleaseYear = atlTrack.Year,
                DiscTotal = atlTrack.DiscTotal,
                TrackTotal = atlTrack.TrackTotal,
                DateIndexed = DateTime.UtcNow,
                CoverFilePath = GetAlbumArtwork(atlTrack)
            };
            _context.Albums.Add(indexedAlbum);
        }

        if (!indexedAlbum.Artists
                .OrderBy(a => a.Name)
                .SequenceEqual(artists.OrderBy(a => a.Name)))
        {
            var missingArtists = artists.Where(a => !indexedAlbum.Artists.Contains(a));
            _context.Artists.AddRange(missingArtists);
        }
        return indexedAlbum;
    }
}