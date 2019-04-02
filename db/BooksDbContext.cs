using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace videotheque.db
{
    class BooksDbContext : DbContext
    {

        private static BooksDbContext _context = null;
        public async static Task<BooksDbContext> GetCurrent()
        {
            if (_context == null)
            {
                _context = new BooksDbContext(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "databse.db"));
                await _context.Database.MigrateAsync();
            }
            return _context;
        }

        private BooksDbContext(string databasePath) : base()
        {
            DatabasePath = databasePath;
        }

        public string DatabasePath { get; }

        //public DbSet<Author> Authors { get; set; }
        //public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite($"Filename={DatabasePath}", x => x.SuppressForeignKeyEnforcement());
        }
    }
}
