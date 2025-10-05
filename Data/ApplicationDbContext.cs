// ApplicationDbContext

using Microsoft.EntityFrameworkCore;
using FYP_MusicGame_Backend.Models;

namespace FYP_MusicGame_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public ApplicationDbContext() { }

        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GameHistory> GameHistories { get; set; }
        public DbSet<Chart> Charts { get; set; }
        public DbSet<BugReport> BugReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>()
                .Property(p => p.UploadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .Property(p => p.IsAdmin)
                .HasDefaultValue(false);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
                
            modelBuilder.Entity<User>()
                .Property(p => p.MasterVolume)
                .HasDefaultValue(1.0f);
                
            modelBuilder.Entity<User>()
                .Property(p => p.EffectVolume)
                .HasDefaultValue(1.0f);
                
            modelBuilder.Entity<User>()
                .Property(p => p.MusicVolume)
                .HasDefaultValue(1.0f);

            modelBuilder.Entity<Chart>()
                .Property(p => p.UploadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}