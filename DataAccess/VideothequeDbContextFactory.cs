using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace videotheque.DataAccess
{
    class VideothequeDbContextFactory : IDesignTimeDbContextFactory<VideothequeDbContext>
    {
        public VideothequeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideothequeDbContext>();
            optionsBuilder.UseSqlite("Data Source=database.db");

            return new VideothequeDbContext(optionsBuilder.Options);
        }
    }
}
