﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using messaging_service.Data;

#nullable disable

namespace messaging_service.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240514101600_profile-picture")]
    partial class profilepicture
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("chat")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("messaging_service.Models.Domain.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Token")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WorkspaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("Invitations", "chat");
                });

            modelBuilder.Entity("messaging_service.models.domain.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ChannelString")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime?>("Created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool?>("Is_OneToOne")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_private")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("WorkspaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChannelString")
                        .IsUnique();

                    b.HasIndex("WorkspaceId", "Name")
                        .IsUnique();

                    b.ToTable("Channels", "chat");
                });

            modelBuilder.Entity("messaging_service.models.domain.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Attachement_Key")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Attachement_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Attachement_Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<bool>("Is_deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Modified_at")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("ParentId");

                    b.HasIndex("UserId");

                    b.ToTable("Chats", "chat");
                });

            modelBuilder.Entity("messaging_service.models.domain.Member", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ChannelId");

                    b.HasIndex("ChannelId");

                    b.ToTable("Members", "chat");
                });

            modelBuilder.Entity("messaging_service.models.domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Last_login")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NotificationString")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthId")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NotificationString")
                        .IsUnique();

                    b.ToTable("Users", "chat");
                });

            modelBuilder.Entity("messaging_service.models.domain.UserWorkspace", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WorkspaceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("UserId", "WorkspaceId");

                    b.HasIndex("WorkspaceId");

                    b.HasIndex("UserId", "WorkspaceId")
                        .IsUnique();

                    b.ToTable("UsersWorkspaces", "chat");
                });

            modelBuilder.Entity("messaging_service.models.domain.Workspace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created_at")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Workspaces", "chat");
                });

            modelBuilder.Entity("messaging_service.Models.Domain.Invitation", b =>
                {
                    b.HasOne("messaging_service.models.domain.User", "User")
                        .WithMany("Invitations")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.HasOne("messaging_service.models.domain.Workspace", "Workspace")
                        .WithMany("Invitations")
                        .HasForeignKey("WorkspaceId")
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("messaging_service.models.domain.Channel", b =>
                {
                    b.HasOne("messaging_service.models.domain.Workspace", "Workspace")
                        .WithMany("Channels")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("messaging_service.models.domain.Chat", b =>
                {
                    b.HasOne("messaging_service.models.domain.Channel", "Channel")
                        .WithMany("Messages")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("messaging_service.models.domain.Chat", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("messaging_service.models.domain.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("Parent");

                    b.Navigation("User");
                });

            modelBuilder.Entity("messaging_service.models.domain.Member", b =>
                {
                    b.HasOne("messaging_service.models.domain.Channel", "Channel")
                        .WithMany("Members")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("messaging_service.models.domain.User", "User")
                        .WithMany("Memberships")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("messaging_service.models.domain.UserWorkspace", b =>
                {
                    b.HasOne("messaging_service.models.domain.User", "User")
                        .WithMany("UserWorkspaces")
                        .HasForeignKey("UserId")
                        .IsRequired();

                    b.HasOne("messaging_service.models.domain.Workspace", "Workspace")
                        .WithMany("UserWorkspaces")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("messaging_service.models.domain.Workspace", b =>
                {
                    b.HasOne("messaging_service.models.domain.User", "Admin")
                        .WithMany("WorkspacesAdmin")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("messaging_service.models.domain.Channel", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("messaging_service.models.domain.Chat", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("messaging_service.models.domain.User", b =>
                {
                    b.Navigation("Invitations");

                    b.Navigation("Memberships");

                    b.Navigation("Messages");

                    b.Navigation("UserWorkspaces");

                    b.Navigation("WorkspacesAdmin");
                });

            modelBuilder.Entity("messaging_service.models.domain.Workspace", b =>
                {
                    b.Navigation("Channels");

                    b.Navigation("Invitations");

                    b.Navigation("UserWorkspaces");
                });
#pragma warning restore 612, 618
        }
    }
}
