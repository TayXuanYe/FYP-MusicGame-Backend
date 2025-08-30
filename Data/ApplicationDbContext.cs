using Microsoft.EntityFrameworkCore;
using FYP_MusicGame_Backend.Models;

namespace FYP_MusicGame_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<DbContext> options) : base(options) { }

        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GameHistory> GameHistories { get; set; }
        public DbSet<UserGameSetting> UserGameSetting { get; set; }
        public DbSet<Chart> Charts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>()
                .Property(p => p.UploadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Chart>()
                .Property(p => p.UploadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}