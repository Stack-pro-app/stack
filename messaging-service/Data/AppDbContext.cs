﻿using messaging_service.models.domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Xml;


namespace messaging_service.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        public AppDbContext():base() { }

        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Workspace> Workspaces { get; set; }
        public virtual DbSet<UserWorkspace> UsersWorkspaces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("chat");
            var entityTypes = new[]
            {
                    typeof(Channel),
                    typeof(Chat),
                    typeof(Member),
                    typeof(User),
                    typeof(Workspace),
                    typeof(UserWorkspace)
            };

            modelBuilder.Entity<Channel>()
            .Property(c => c.ChannelString)
            .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Workspace>()
            .HasIndex(w => new { w.Name, w.AdminId })
            .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.AuthId)
                .IsUnique();

            modelBuilder.Entity<UserWorkspace>()
            .HasIndex(uw => new { uw.UserId, uw.WorkspaceId })
            .IsUnique();


            modelBuilder.Entity<Channel>()
            .HasIndex(c => new { c.WorkspaceId, c.Name })
            .IsUnique();

            modelBuilder.Entity<Channel>()
            .HasIndex(c => c.ChannelString)
            .IsUnique();

            modelBuilder
            .Entity<Chat>()
            .HasOne(e => e.Parent)
            .WithMany(e => e.Children)
            .OnDelete(DeleteBehavior.ClientSetNull);



            foreach (var entityType in entityTypes)
            {
                modelBuilder.Entity(entityType)
                    .Property("Created_at")
                    .HasDefaultValueSql("getdate()");
            }

        }



    }
}
