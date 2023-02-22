﻿using System.Net;
using Coral.Database.Models;
using Coral.Dto.EncodingModels;
using Coral.Dto.Models;
using Coral.Services;
using Coral.Services.Helpers;
using Coral.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coral.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        private readonly ITranscoderService _transcoderService;
        private readonly ISearchService _searchService;
        private readonly IIndexerService _indexerService;

        public LibraryController(ILibraryService libraryService, ITranscoderService transcoderService, ISearchService searchService, IIndexerService indexerService)
        {
            _libraryService = libraryService;
            _transcoderService = transcoderService;
            _searchService = searchService;
            _indexerService = indexerService;
        }

        [HttpPost]
        [Route("scan")]
        public async Task<ActionResult> RunIndexer()
        {
            var contentDirectory = Environment.GetEnvironmentVariable("CORAL_CONTENT_DIRECTORY");
            if (!string.IsNullOrEmpty(contentDirectory))
            {
                await _indexerService.ReadDirectory(contentDirectory);
                return Ok();
            }
            else
            {
                return BadRequest(new { Message = "CORAL_CONTENT_DIRECTORY has not been set." });
            }
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<SearchResult>> Search([FromQuery] string query)
        {
            var searchResult = await _searchService.Search(query);
            return Ok(searchResult);
        }

        [HttpGet, HttpHead]
        [Route("tracks/{trackId}/original")]
        public async Task<ActionResult> FileFromLibrary(int trackId)
        {
            try
            {
                var trackStream = await _libraryService.GetStreamForTrack(trackId);
                return File(trackStream.Stream, trackStream.ContentType, true);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("tracks/{trackId}/transcode")]
        public async Task<ActionResult<StreamDto>> TranscodeTrack(int trackId, int bitrate)
        {
            var dbTrack = await _libraryService.GetTrack(trackId);
            if (dbTrack == null)
            {
                return NotFound(new
                {
                    Message = "Track not found."
                });
            }

            var job = await _transcoderService.CreateJob(OutputFormat.AAC, opt =>
            {
                opt.SourceTrack = dbTrack;
                opt.Bitrate = bitrate;
                opt.RequestType = TranscodeRequestType.HLS;
            });
            
            var streamData = new StreamDto()
            {
                // this will require some baseurl modifications via the web server
                // responsible for reverse proxying Coral
                Link = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/hls/{job.Id}/{job.FinalOutputFile}",
                TranscodeInfo = new TranscodeInfoDto()
                {
                    JobId = job.Id,
                    Bitrate = job.Request.Bitrate,
                    Format = OutputFormat.AAC
                }
            };

            return streamData;
        }

        [HttpGet]
        [Route("tracks/{trackId}/stream")]
        public ActionResult<StreamDto> StreamTrack(int trackId,
            [FromQuery] int bitrate = 192,
            [FromQuery] bool transcodeTrack = true)
        {
            if (!transcodeTrack)
            {
                return new StreamDto()
                {
                    Link = Url.Action("FileFromLibrary", "Library", new
                    {
                        trackId = trackId
                    }, Request.Scheme)!,
                    TranscodeInfo = null,
                };
            }

            return RedirectToAction("TranscodeTrack", new {trackId = trackId, bitrate = bitrate});
        }

        [HttpGet]
        [Route("tracks")]
        public async IAsyncEnumerable<TrackDto> Tracks()
        {
            await foreach (var track in _libraryService.GetTracks())
            {
                yield return track;
            }
        }

        [HttpGet]
        [Route("albums")]
        public async IAsyncEnumerable<SimpleAlbumDto> Albums()
        {
            await foreach (var album in _libraryService.GetAlbums())
            {
                yield return album;
            }
        }

        [HttpGet]
        [Route("albums/paginated")]
        public async Task<ActionResult<PaginatedData<List<SimpleAlbumDto>>>> PaginatedAlbums([FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var result = await _libraryService.GetPaginatedAlbums(offset, limit);
            return Ok(result);
        }

        [HttpGet]
        [Route("albums/{albumId}")]
        public async Task<ActionResult<AlbumDto>> Album(int albumId)
        {
            var album = await _libraryService.GetAlbum(albumId);
            if (album == null)
            {
                return NotFound();
            }

            return album;
        }
    }
}