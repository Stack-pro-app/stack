using messaging_service.models.domain;
using messaging_service.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Xml;


namespace messaging_service.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<UserWorkspace> UsersWorkspaces { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

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

            // Configure UserWorkspace
            modelBuilder.Entity<UserWorkspace>()
                .HasKey(uw => new { uw.UserId, uw.WorkspaceId });

            modelBuilder.Entity<UserWorkspace>()
                .HasOne<User>(uw => uw.User)
                .WithMany(u => u.UserWorkspaces)
                .HasForeignKey(uw => uw.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<UserWorkspace>()
                .HasOne<Workspace>(uw => uw.Workspace)
                .WithMany(w => w.UserWorkspaces)
                .HasForeignKey(uw => uw.WorkspaceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Configure Member
            modelBuilder.Entity<Member>()
                .HasKey(m => new { m.UserId, m.ChannelId });

            modelBuilder.Entity<Member>()
                .HasOne<User>(m => m.User)
                .WithMany(u => u.Memberships)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Member>()
                .HasOne<Channel>(m => m.Channel)
                .WithMany(c => c.Members)
                .HasForeignKey(m => m.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Chat
            modelBuilder.Entity<Chat>()
                .HasOne<User>(c => c.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Chat>()
                .HasOne<Channel>(c => c.Channel)
                .WithMany(c => c.Messages)
                .HasForeignKey(c => c.ChannelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chat>()
                .HasOne<Chat>(c => c.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Workspace>()
                .HasMany(w => w.UserWorkspaces)
                .WithOne(uw => uw.Workspace)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Invitation
            modelBuilder.Entity<Invitation>()
                .HasOne<User>(i => i.User)
                .WithMany(u => u.Invitations)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Invitation>()
                .HasOne<Workspace>(i => i.Workspace)
                .WithMany(w => w.Invitations)
                .HasForeignKey(i => i.WorkspaceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Channel>()
            .Property(c => c.ChannelString)
            .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<User>()
            .Property(u => u.NotificationString)
            .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Invitation>()
            .Property(i => i.Token)
            .HasDefaultValueSql("NEWID()");

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

            modelBuilder.Entity<User>()
            .HasIndex(u => u.NotificationString)
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
