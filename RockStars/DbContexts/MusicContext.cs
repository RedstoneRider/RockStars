using Microsoft.EntityFrameworkCore;
using RockStars.Modals;

namespace RockStars.DbContexts
{
    public class MusicContext : DbContext
    {
        public MusicContext(DbContextOptions<MusicContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adding unique key for artist name
            modelBuilder.Entity<Artist>()
                .HasIndex(a => a.Name)
                .IsUnique();

            // Adding unique key for song name
            modelBuilder.Entity<Song>()
                .HasIndex(s => s.Name)
                .IsUnique();
        }
    }
}
