﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchedItApi.Data;

#nullable disable

namespace WatchedItApi.Data.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230325122725_userVotesUpdate")]
    partial class userVotesUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MovieListUser", b =>
                {
                    b.Property<int>("MovieListId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("MovieListId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("MovieListUser");
                });

            modelBuilder.Entity("WatchedItApi.Models.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("WatchedItApi.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExternId")
                        .HasColumnType("int");

                    b.Property<int>("MovieListId")
                        .HasColumnType("int");

                    b.Property<bool>("movie")
                        .HasColumnType("bit");

                    b.Property<int>("voteCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieListId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("WatchedItApi.Models.MovieList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("voteDeadLine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("watchDateTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovieLists");
                });

            modelBuilder.Entity("WatchedItApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WatchedItApi.Models.UserVote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MovieListId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.Property<bool>("voted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("UserVotes");
                });

            modelBuilder.Entity("MovieListUser", b =>
                {
                    b.HasOne("WatchedItApi.Models.MovieList", null)
                        .WithMany()
                        .HasForeignKey("MovieListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WatchedItApi.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WatchedItApi.Models.Friend", b =>
                {
                    b.HasOne("WatchedItApi.Models.User", null)
                        .WithMany("Friends")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WatchedItApi.Models.Movie", b =>
                {
                    b.HasOne("WatchedItApi.Models.MovieList", null)
                        .WithMany("Movies")
                        .HasForeignKey("MovieListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WatchedItApi.Models.MovieList", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("WatchedItApi.Models.User", b =>
                {
                    b.Navigation("Friends");
                });
#pragma warning restore 612, 618
        }
    }
}
