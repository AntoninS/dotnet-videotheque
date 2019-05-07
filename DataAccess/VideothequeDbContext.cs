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
                    Path.Combine(GetFolderPath(SpecialFolder.LocalApplicationData), "database.db"));
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

        public DbSet<classes.Episode> Episodes { get; set; }
        public DbSet<classes.Genre> Genres { get; set; }
        public DbSet<classes.GenreMedia> GenreMedias { get; set; }
        public DbSet<classes.Media> Medias { get; set; }
        public DbSet<classes.Personne> Personnes { get; set; }
        public DbSet<classes.PersonneMedia> PersonneMedias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<classes.GenreMedia>()
                                .HasKey(gm => new { gm.IdGenre, gm.IdMedia });

            modelBuilder.Entity<classes.PersonneMedia>()
                                .HasKey(pm => new { pm.IdPersonne, pm.IdMedia});

            //modelBuilder.Entity<Model.AuthorBook>().HasOne(ab => ab.Book)
            //                                       .WithMany(b => b.Authors)
            //                                       .HasForeignKey(ab => ab.BookId);
        }
    }
}
