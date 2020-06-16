using Microsoft.EntityFrameworkCore;

namespace Test2_Example.Models
{
    public class MusicDbContext : DbContext
    {
        
        public DbSet<MusicLabel> MusicLabel { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Musician_Track> Musician_Track { get; set; }
        public DbSet<Musician> Musician { get; set; }

        public MusicDbContext()
        {
        }

        public MusicDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Musician_Track>()
                .HasKey(e => new {e.IdMusician, e.IdTrack});
        }
    }
}