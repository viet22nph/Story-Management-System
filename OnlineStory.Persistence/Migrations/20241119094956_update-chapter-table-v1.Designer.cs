﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineStory.Persistence.ApplicationDbContext;

#nullable disable

namespace OnlineStory.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241119094956_update-chapter-table-v1")]
    partial class updatechaptertablev1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.ChapterImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChapterId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("UploadedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.ToTable("ChapterImage", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Identity.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeleteAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("[IsDeleted] = 0");

                    b.ToTable("AppUser", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AppRoleClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AppUserClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AppUserLogin", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AppUserToken", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Chapter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChapterNumber")
                        .HasColumnType("int");

                    b.Property<string>("ChapterTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("CreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UpdateBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StoryId");

                    b.ToTable("Chapter", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ChapterId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Left")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("int")
                        .HasComment("Id comment parent if null this is parent");

                    b.Property<int>("Right")
                        .HasColumnType("int");

                    b.Property<Guid?>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasComment("Loại bình luận: 1 là bình luận truyện, 2 là bình luận chương");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("StoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("Slug", "Name")
                        .IsUnique();

                    b.ToTable("Genre", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Identity.Action", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Action", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Identity.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeleteAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("UpdateBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted")
                        .HasFilter("[IsDeleted] = 0");

                    b.ToTable("AppRole", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Identity.AppUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AppUserRole", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Identity.Permission", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ActionId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "ActionId", "ResourceId");

                    b.HasIndex("ActionId");

                    b.HasIndex("ResourceId");

                    b.ToTable("Permission", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Identity.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Resource", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("CreateAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsBatch")
                        .HasColumnType("bit");

                    b.Property<bool>("IsGlobal")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("RelatedData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasComment("Notification types: NewChapter, Comment, ChapterUpdate, System");

                    b.Property<Guid>("UserReceiveId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserReceiveId");

                    b.ToTable("Notification", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.ReadingHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChapterId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("LastReadAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.HasIndex("UserId");

                    b.HasIndex("StoryId", "UserId", "ChapterId")
                        .IsUnique();

                    b.ToTable("ReadingHistory", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Story", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnotherStoryTitle")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Audience")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasDefaultValue("Both");

                    b.Property<string>("Author")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeleteAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("StoryStatus")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("StoryTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("IsDeleted")
                        .HasFilter("[IsDeleted] = 0");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Story", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.StoryGenre", b =>
                {
                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("StoryId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("StoryGenre", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.StoryRecommendations", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<double>("Score")
                        .HasColumnType("float");

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StoryId");

                    b.HasIndex("UserId");

                    b.ToTable("StoryRecommendations", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.StoryView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ViewDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StoryId", "ViewDate")
                        .IsUnique();

                    b.ToTable("StoryView", (string)null);
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.UserStoryTracking", b =>
                {
                    b.Property<Guid>("StoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("FollowAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsNotificationEnabled")
                        .HasColumnType("bit");

                    b.HasKey("StoryId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserStoryTracking", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.ChapterImage", b =>
                {
                    b.HasOne("OnlineStory.Domain.Entities.Chapter", "Chapter")
                        .WithMany("Images")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chapter");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("OnlineStory.Domain.Entities.Identity.AppRole", null)
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Domain.Entities.Identity.AppUser", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Domain.Entities.Identity.AppUser", null)
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Domain.Entities.Identity.AppUser", null)
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Chapter", b =>
                {
                    b.HasOne("OnlineStory.Domain.Entities.Story", "Story")
                        .WithMany("Chapters")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Story");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Comment", b =>
                {
                    b.HasOne("OnlineStory.Domain.Entities.Chapter", "Chapter")
                        .WithMany("Comments")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineStory.Domain.Entities.Comment", "ParentComment")
                        .WithMany("Replies")
                        .HasForeignKey("ParentCommentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("OnlineStory.Domain.Entities.Story", "Story")
                        .WithMany("Comments")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.Identity.AppUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chapter");

                    b.Navigation("ParentComment");

                    b.Navigation("Story");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Identity.AppUserRole", b =>
                {
                    b.HasOne("OnlineStory.Domain.Entities.Identity.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Identity.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Identity.Permission", b =>
                {
                    b.HasOne("OnlineStory.Domain.Entities.Identity.Action", "Action")
                        .WithMany("Permissions")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineStory.Domain.Entities.Identity.Resource", "Resource")
                        .WithMany("Permissions")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineStory.Domain.Entities.Identity.AppRole", "Role")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("Resource");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Notification", b =>
                {
                    b.HasOne("Domain.Entities.Identity.AppUser", "UserReceive")
                        .WithMany("Notifications")
                        .HasForeignKey("UserReceiveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("User_Notification");

                    b.Navigation("UserReceive");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.ReadingHistory", b =>
                {
                    b.HasOne("OnlineStory.Domain.Entities.Chapter", "Chapter")
                        .WithMany("ReadingHistories")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineStory.Domain.Entities.Story", "Story")
                        .WithMany("Histories")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Identity.AppUser", "User")
                        .WithMany("ReadingHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chapter");

                    b.Navigation("Story");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Story", b =>
                {
                    b.HasOne("OnlineStory.Domain.Entities.Country", "Country")
                        .WithMany("Stories")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.StoryGenre", b =>
                {
                    b.HasOne("OnlineStory.Domain.Entities.Genre", "Genre")
                        .WithMany("StoryGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OnlineStory.Domain.Entities.Story", "Story")
                        .WithMany("StoryGenres")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Story");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.StoryRecommendations", b =>
                {
                    b.HasOne("OnlineStory.Domain.Entities.Story", "Story")
                        .WithMany("StoryRecommendations")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Identity.AppUser", "User")
                        .WithMany("StoryRecommendations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Story");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.StoryView", b =>
                {
                    b.HasOne("OnlineStory.Domain.Entities.Story", "Story")
                        .WithMany("StoryViews")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Story");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.UserStoryTracking", b =>
                {
                    b.HasOne("OnlineStory.Domain.Entities.Story", "Story")
                        .WithMany("FollowStories")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Identity.AppUser", "User")
                        .WithMany("UserStoryTrackings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Story");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Identity.AppUser", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Comments");

                    b.Navigation("Logins");

                    b.Navigation("Notifications");

                    b.Navigation("ReadingHistories");

                    b.Navigation("StoryRecommendations");

                    b.Navigation("UserRoles");

                    b.Navigation("UserStoryTrackings");

                    b.Navigation("UserTokens");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Chapter", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Images");

                    b.Navigation("ReadingHistories");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Comment", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Country", b =>
                {
                    b.Navigation("Stories");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Genre", b =>
                {
                    b.Navigation("StoryGenres");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Identity.Action", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Identity.AppRole", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Permissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Identity.Resource", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("OnlineStory.Domain.Entities.Story", b =>
                {
                    b.Navigation("Chapters");

                    b.Navigation("Comments");

                    b.Navigation("FollowStories");

                    b.Navigation("Histories");

                    b.Navigation("StoryGenres");

                    b.Navigation("StoryRecommendations");

                    b.Navigation("StoryViews");
                });
#pragma warning restore 612, 618
        }
    }
}
