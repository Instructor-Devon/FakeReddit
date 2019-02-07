﻿// <auto-generated />
using System;
using FakeReddit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FakeReddit.Migrations
{
    [DbContext(typeof(MainContext))]
    partial class MainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FakeReddit.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("FakeReddit.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("PostId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("FakeReddit.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FakeReddit.Models.Vote", b =>
                {
                    b.Property<int>("VoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsUpvote");

                    b.Property<int>("PostId");

                    b.Property<int>("UserId");

                    b.HasKey("VoteId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("FakeReddit.Models.Post", b =>
                {
                    b.HasOne("FakeReddit.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FakeReddit.Models.User", "Author")
                        .WithMany("PublishedPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FakeReddit.Models.Vote", b =>
                {
                    b.HasOne("FakeReddit.Models.Post", "VotedPost")
                        .WithMany("Votes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FakeReddit.Models.User", "Voter")
                        .WithMany("PostsVoted")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
