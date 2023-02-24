﻿// <auto-generated />
using System;
using Coral.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Coral.Database.Migrations
{
    [DbContext(typeof(CoralDbContext))]
    partial class CoralDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("AlbumArtistWithRole", b =>
                {
                    b.Property<Guid>("AlbumsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArtistsId")
                        .HasColumnType("TEXT");

                    b.HasKey("AlbumsId", "ArtistsId");

                    b.HasIndex("ArtistsId");

                    b.ToTable("AlbumArtistWithRole");
                });

            modelBuilder.Entity("ArtistWithRoleTrack", b =>
                {
                    b.Property<Guid>("ArtistsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TracksId")
                        .HasColumnType("TEXT");

                    b.HasKey("ArtistsId", "TracksId");

                    b.HasIndex("TracksId");

                    b.ToTable("ArtistWithRoleTrack");
                });

            modelBuilder.Entity("Coral.Database.Models.BaseTable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateIndexed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(new DateTime(2023, 2, 24, 10, 39, 57, 94, DateTimeKind.Utc).AddTicks(6983));

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(new DateTime(2023, 2, 24, 10, 39, 57, 94, DateTimeKind.Utc).AddTicks(6641));

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("KeywordTrack", b =>
                {
                    b.Property<Guid>("KeywordsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TracksId")
                        .HasColumnType("TEXT");

                    b.HasKey("KeywordsId", "TracksId");

                    b.HasIndex("TracksId");

                    b.ToTable("KeywordTrack");
                });

            modelBuilder.Entity("Coral.Database.Models.Album", b =>
                {
                    b.HasBaseType("Coral.Database.Models.BaseTable");

                    b.Property<string>("CoverFilePath")
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

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Coral.Database.Models.Artist", b =>
                {
                    b.HasBaseType("Coral.Database.Models.BaseTable");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("Coral.Database.Models.ArtistWithRole", b =>
                {
                    b.HasBaseType("Coral.Database.Models.BaseTable");

                    b.Property<Guid>("ArtistId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ArtistId");

                    b.ToTable("ArtistsWithRoles");
                });

            modelBuilder.Entity("Coral.Database.Models.Artwork", b =>
                {
                    b.HasBaseType("Coral.Database.Models.BaseTable");

                    b.Property<Guid>("AlbumId")
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

                    b.HasIndex("AlbumId");

                    b.ToTable("Artworks");
                });

            modelBuilder.Entity("Coral.Database.Models.Genre", b =>
                {
                    b.HasBaseType("Coral.Database.Models.BaseTable");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Coral.Database.Models.Keyword", b =>
                {
                    b.HasBaseType("Coral.Database.Models.BaseTable");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasIndex("Value");

                    b.ToTable("Keyword", (string)null);
                });

            modelBuilder.Entity("Coral.Database.Models.Track", b =>
                {
                    b.HasBaseType("Coral.Database.Models.BaseTable");

                    b.Property<Guid>("AlbumId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DiscNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DurationInSeconds")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("GenreId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("TrackNumber")
                        .HasColumnType("INTEGER");

                    b.HasIndex("AlbumId");

                    b.HasIndex("GenreId");

                    b.ToTable("Tracks");
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

            modelBuilder.Entity("Coral.Database.Models.ArtistWithRole", b =>
                {
                    b.HasOne("Coral.Database.Models.Artist", "Artist")
                        .WithMany()
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

            modelBuilder.Entity("Coral.Database.Models.Track", b =>
                {
                    b.HasOne("Coral.Database.Models.Album", "Album")
                        .WithMany("Tracks")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Coral.Database.Models.Genre", "Genre")
                        .WithMany("Tracks")
                        .HasForeignKey("GenreId");

                    b.Navigation("Album");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Coral.Database.Models.Album", b =>
                {
                    b.Navigation("Artworks");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("Coral.Database.Models.Genre", b =>
                {
                    b.Navigation("Tracks");
                });
#pragma warning restore 612, 618
        }
    }
}
