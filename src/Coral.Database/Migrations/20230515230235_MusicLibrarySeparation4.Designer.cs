﻿// <auto-generated />
using System;
using Coral.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Coral.Database.Migrations
{
    [DbContext(typeof(CoralDbContext))]
    [Migration("20230515230235_MusicLibrarySeparation4")]
    partial class MusicLibrarySeparation4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("AlbumArtistWithRole", b =>
                {
                    b.Property<int>("AlbumsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArtistsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AlbumsId", "ArtistsId");

                    b.HasIndex("ArtistsId");

                    b.ToTable("AlbumArtistWithRole");
                });

            modelBuilder.Entity("ArtistWithRoleTrack", b =>
                {
                    b.Property<int>("ArtistsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TracksId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArtistsId", "TracksId");

                    b.HasIndex("TracksId");

                    b.ToTable("ArtistWithRoleTrack");
                });

            modelBuilder.Entity("Coral.Database.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CoverFilePath")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateIndexed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DiscTotal")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ReleaseYear")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TrackTotal")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Type");

                    b.ToTable("Album", (string)null);
                });

            modelBuilder.Entity("Coral.Database.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateIndexed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Artist", (string)null);
                });

            modelBuilder.Entity("Coral.Database.Models.ArtistWithRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArtistId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateIndexed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("ArtistWithRole", (string)null);
                });

            modelBuilder.Entity("Coral.Database.Models.Artwork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AlbumId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateIndexed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Size")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Width")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Artwork", (string)null);
                });

            modelBuilder.Entity("Coral.Database.Models.AudioFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AudioMetadataId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateIndexed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("FileSizeInBytes")
                        .HasColumnType("TEXT");

                    b.Property<int>("LibraryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AudioMetadataId");

                    b.HasIndex("LibraryId");

                    b.ToTable("AudioFile", (string)null);
                });

            modelBuilder.Entity("Coral.Database.Models.AudioMetadata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BitDepth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Bitrate")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Channels")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Codec")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateIndexed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<double>("SampleRate")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("AudioMetadata", (string)null);
                });

            modelBuilder.Entity("Coral.Database.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateIndexed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genre", (string)null);
                });

            modelBuilder.Entity("Coral.Database.Models.Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateIndexed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Value");

                    b.ToTable("Keyword", (string)null);
                });

            modelBuilder.Entity("Coral.Database.Models.MusicLibrary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateIndexed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastScan")
                        .HasColumnType("TEXT");

                    b.Property<string>("LibraryPath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MusicLibrary", (string)null);
                });

            modelBuilder.Entity("Coral.Database.Models.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AlbumId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AudioFileId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateIndexed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DiscNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DurationInSeconds")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GenreId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("TrackNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("AudioFileId");

                    b.HasIndex("GenreId");

                    b.ToTable("Track", (string)null);
                });

            modelBuilder.Entity("KeywordTrack", b =>
                {
                    b.Property<int>("KeywordsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TracksId")
                        .HasColumnType("INTEGER");

                    b.HasKey("KeywordsId", "TracksId");

                    b.HasIndex("TracksId");

                    b.ToTable("KeywordTrack");
                });

            modelBuilder.Entity("AlbumArtistWithRole", b =>
                {
                    b.HasOne("Coral.Database.Models.Album", null)
                        .WithMany()
                        .HasForeignKey("AlbumsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coral.Database.Models.ArtistWithRole", null)
                        .WithMany()
                        .HasForeignKey("ArtistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArtistWithRoleTrack", b =>
                {
                    b.HasOne("Coral.Database.Models.ArtistWithRole", null)
                        .WithMany()
                        .HasForeignKey("ArtistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coral.Database.Models.Track", null)
                        .WithMany()
                        .HasForeignKey("TracksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coral.Database.Models.ArtistWithRole", b =>
                {
                    b.HasOne("Coral.Database.Models.Artist", "Artist")
                        .WithMany("Roles")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Coral.Database.Models.Artwork", b =>
                {
                    b.HasOne("Coral.Database.Models.Album", "Album")
                        .WithMany("Artworks")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("Coral.Database.Models.AudioFile", b =>
                {
                    b.HasOne("Coral.Database.Models.AudioMetadata", "AudioMetadata")
                        .WithMany()
                        .HasForeignKey("AudioMetadataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coral.Database.Models.MusicLibrary", "Library")
                        .WithMany("AudioFiles")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AudioMetadata");

                    b.Navigation("Library");
                });

            modelBuilder.Entity("Coral.Database.Models.Track", b =>
                {
                    b.HasOne("Coral.Database.Models.Album", "Album")
                        .WithMany("Tracks")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coral.Database.Models.AudioFile", "AudioFile")
                        .WithMany()
                        .HasForeignKey("AudioFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coral.Database.Models.Genre", "Genre")
                        .WithMany("Tracks")
                        .HasForeignKey("GenreId");

                    b.Navigation("Album");

                    b.Navigation("AudioFile");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("KeywordTrack", b =>
                {
                    b.HasOne("Coral.Database.Models.Keyword", null)
                        .WithMany()
                        .HasForeignKey("KeywordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coral.Database.Models.Track", null)
                        .WithMany()
                        .HasForeignKey("TracksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Coral.Database.Models.Album", b =>
                {
                    b.Navigation("Artworks");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("Coral.Database.Models.Artist", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Coral.Database.Models.Genre", b =>
                {
                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("Coral.Database.Models.MusicLibrary", b =>
                {
                    b.Navigation("AudioFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
