using messaging_service.models.domain;
using Microsoft.EntityFrameworkCore;


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

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.AuthId)
                .IsUnique();

            modelBuilder.Entity<UserWorkspace>()
            .HasIndex(uw => new { uw.UserId, uw.WorkspaceId })
            .IsUnique();

            foreach (var entityType in entityTypes)
            {
                modelBuilder.Entity(entityType)
                    .Property("Created_at")
                    .HasDefaultValueSql("getdate()");
            }

        }



    }
}
