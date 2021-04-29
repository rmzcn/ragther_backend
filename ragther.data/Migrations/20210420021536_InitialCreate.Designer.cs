﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ragther.data.Concrete.EFCore;

namespace ragther.data.Migrations
{
    [DbContext(typeof(RagtherDbContext))]
    [Migration("20210420021536_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ragther.entity.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("varchar(500) CHARACTER SET utf8mb4")
                        .HasMaxLength(500);

                    b.Property<bool>("IsOffer")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("int");

                    b.Property<int>("TodoId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("TodoId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("ragther.entity.Friendship", b =>
                {
                    b.Property<int>("FriendshipKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FriendshipConditionId")
                        .HasColumnType("int");

                    b.Property<int>("RecipientUserId")
                        .HasColumnType("int");

                    b.Property<int>("SenderUserId")
                        .HasColumnType("int");

                    b.HasKey("FriendshipKey");

                    b.HasIndex("FriendshipConditionId");

                    b.HasIndex("RecipientUserId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Friendship");
                });

            modelBuilder.Entity("ragther.entity.FriendshipCondition", b =>
                {
                    b.Property<int>("ConditionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ConditionId");

                    b.ToTable("FriendshipCondition");
                });

            modelBuilder.Entity("ragther.entity.Like", b =>
                {
                    b.Property<int>("TodoId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LikeDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("TodoId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("ragther.entity.MailUpdate", b =>
                {
                    b.Property<int>("MailUpdateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MailWillUpdate")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Token")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TokenConditionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MailUpdateId");

                    b.HasIndex("TokenConditionId");

                    b.HasIndex("UserId");

                    b.ToTable("MailUpdate");
                });

            modelBuilder.Entity("ragther.entity.Notice", b =>
                {
                    b.Property<int>("NoticeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("NoticeTypeId")
                        .HasColumnType("int");

                    b.Property<int>("OwnerUserId")
                        .HasColumnType("int");

                    b.Property<string>("RelevantURL")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RelevantUserId")
                        .HasColumnType("int");

                    b.HasKey("NoticeId");

                    b.HasIndex("NoticeTypeId");

                    b.HasIndex("OwnerUserId");

                    b.HasIndex("RelevantUserId");

                    b.ToTable("Notice");
                });

            modelBuilder.Entity("ragther.entity.NoticeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("NoticeType");
                });

            modelBuilder.Entity("ragther.entity.ProfileDetail", b =>
                {
                    b.Property<int>("ProfileDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FriendCount")
                        .HasColumnType("int");

                    b.Property<int>("HelpCount")
                        .HasColumnType("int");

                    b.Property<string>("HiddenProfileDescription")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsHiddenProfile")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ProfileDescription")
                        .HasColumnType("varchar(400) CHARACTER SET utf8mb4")
                        .HasMaxLength(400);

                    b.Property<int>("ProfileScore")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProfileDetailId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ProfileDetail");
                });

            modelBuilder.Entity("ragther.entity.Remind", b =>
                {
                    b.Property<int>("TodoId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RemindDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("TodoId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Remind");
                });

            modelBuilder.Entity("ragther.entity.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("TagId");

                    b.HasIndex("CreatorUserId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("ragther.entity.TagsOfInterest", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TagId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TagsOfInterest");
                });

            modelBuilder.Entity("ragther.entity.Todo", b =>
                {
                    b.Property<int>("TodoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorUserId")
                        .HasColumnType("int");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TodoConditionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UntilWhen")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("imageUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("TodoId");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("TodoConditionId");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("ragther.entity.TodoCondition", b =>
                {
                    b.Property<int>("ConditionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ConditionId");

                    b.ToTable("TodoCondition");
                });

            modelBuilder.Entity("ragther.entity.TodoTag", b =>
                {
                    b.Property<int>("TodoId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("TodoId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("TodoTag");
                });

            modelBuilder.Entity("ragther.entity.TokenCondition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("TokenCondition");
                });

            modelBuilder.Entity("ragther.entity.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(25) CHARACTER SET utf8mb4")
                        .HasMaxLength(25);

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(22) CHARACTER SET utf8mb4")
                        .HasMaxLength(22);

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ragther.entity.WorkWith", b =>
                {
                    b.Property<int>("TodoId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TodoId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("WorkWith");
                });

            modelBuilder.Entity("ragther.entity.Comment", b =>
                {
                    b.HasOne("ragther.entity.Comment", "ParentComment")
                        .WithMany("Comments")
                        .HasForeignKey("ParentCommentId");

                    b.HasOne("ragther.entity.Todo", "Todo")
                        .WithMany("Comments")
                        .HasForeignKey("TodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ragther.entity.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ragther.entity.Friendship", b =>
                {
                    b.HasOne("ragther.entity.FriendshipCondition", "FriendshipCondition")
                        .WithMany("Friendships")
                        .HasForeignKey("FriendshipConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ragther.entity.User", "RecipientUser")
                        .WithMany("ReceivedRequests")
                        .HasForeignKey("RecipientUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ragther.entity.User", "SenderUser")
                        .WithMany("SendedRequests")
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ragther.entity.Like", b =>
                {
                    b.HasOne("ragther.entity.Todo", "Todo")
                        .WithMany("Likes")
                        .HasForeignKey("TodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ragther.entity.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ragther.entity.MailUpdate", b =>
                {
                    b.HasOne("ragther.entity.TokenCondition", "TokenCondition")
                        .WithMany("MailUpdates")
                        .HasForeignKey("TokenConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ragther.entity.User", "User")
                        .WithMany("MailUpdates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ragther.entity.Notice", b =>
                {
                    b.HasOne("ragther.entity.NoticeType", "NoticeType")
                        .WithMany()
                        .HasForeignKey("NoticeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ragther.entity.User", "OwnerUser")
                        .WithMany("OwnNotices")
                        .HasForeignKey("OwnerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ragther.entity.User", "RelevantUser")
                        .WithMany("RelevantNotices")
                        .HasForeignKey("RelevantUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ragther.entity.ProfileDetail", b =>
                {
                    b.HasOne("ragther.entity.User", "User")
                        .WithOne("ProfileDetail")
                        .HasForeignKey("ragther.entity.ProfileDetail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ragther.entity.Remind", b =>
                {
                    b.HasOne("ragther.entity.Todo", "Todo")
                        .WithMany("Reminds")
                        .HasForeignKey("TodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ragther.entity.User", "User")
                        .WithMany("Reminds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ragther.entity.Tag", b =>
                {
                    b.HasOne("ragther.entity.User", "CreatorUser")
                        .WithMany("CreatedTags")
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ragther.entity.TagsOfInterest", b =>
                {
                    b.HasOne("ragther.entity.Tag", "Tag")
                        .WithMany("TagsOfInterests")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ragther.entity.User", "User")
                        .WithMany("TagsOfInterests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ragther.entity.Todo", b =>
                {
                    b.HasOne("ragther.entity.User", "CreatorUser")
                        .WithMany("Todos")
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ragther.entity.TodoCondition", "TodoCondition")
                        .WithMany("Todos")
                        .HasForeignKey("TodoConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ragther.entity.TodoTag", b =>
                {
                    b.HasOne("ragther.entity.Tag", "Tag")
                        .WithMany("TodoTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ragther.entity.Todo", "Todo")
                        .WithMany("TodoTags")
                        .HasForeignKey("TodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ragther.entity.WorkWith", b =>
                {
                    b.HasOne("ragther.entity.Todo", "Todo")
                        .WithMany("WorkWiths")
                        .HasForeignKey("TodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ragther.entity.User", "User")
                        .WithMany("WorkWiths")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
