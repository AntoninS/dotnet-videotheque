using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

namespace videotheque.DataAccess
{
    public class VideothequeDbContext : DbContext
    { 
    
        private static VideothequeDbContext _context = null;
        public static async Task<VideothequeDbContext> GetCurrent()
        {
            if (_context == null)
            {
                _context = new VideothequeDbContext(
                   Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\Database\\database.db");
                await _context.Database.MigrateAsync();
            }
            return _context;
        }

        internal VideothequeDbContext(DbContextOptions options) : base(options) { }
        private VideothequeDbContext(string databasePath) : base()
        {
            DatabasePath = databasePath;
        }

        public string DatabasePath { get; }

        public DbSet<Model.Episode> Episodes { get; set; }
        public DbSet<Model.Genre> Genres { get; set; }
        public DbSet<Model.GenreMedia> GenreMedias { get; set; }
        public DbSet<Model.Media> Medias { get; set; }
        public DbSet<Model.Personne> Personnes { get; set; }
        public DbSet<Model.PersonneMedia> PersonneMedias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model.Media>()
                                .HasKey(m => new { m.Id });

            modelBuilder.Entity<Model.Genre>()
                                .HasKey(g => new { g.Id });

            modelBuilder.Entity<Model.GenreMedia>().HasKey(gm => new { gm.IdMedia, gm.IdGenre });

            modelBuilder.Entity<Model.GenreMedia>()
                                .HasOne<Model.Media>(gm => gm.Media)
                                .WithMany(m => m.Genres)
                                .HasForeignKey(gm => gm.IdMedia);

            modelBuilder.Entity<Model.GenreMedia>()
                                .HasOne<Model.Genre>(gm => gm.Genre)
                                .WithMany(g => g.GenreMedias)
                                .HasForeignKey(gm => gm.IdGenre);


            modelBuilder.Entity<Model.Personne>()
                                .HasKey(p => new { p.Id });

            modelBuilder.Entity<Model.PersonneMedia>().HasKey(pm => new { pm.IdPersonne, pm.IdMedia });

            modelBuilder.Entity<Model.PersonneMedia>()
                                .HasOne<Model.Media>(pm => pm.Media)
                                .WithMany(m => m.Personnes)
                                .HasForeignKey(pm => pm.IdMedia);

            modelBuilder.Entity<Model.PersonneMedia>()
                                .HasOne<Model.Personne>(pm => pm.Personne)
                                .WithMany(p => p.PersonnesMedia)
                                .HasForeignKey(pm => pm.IdPersonne);

            modelBuilder.Entity<Model.Episode>()
                                .HasKey(e => new { e.Id });

        }
    }
}
