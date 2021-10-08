using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotifyAnalogApp.Data.Models;

namespace SpotifyAnalogApp.Data.Data
{
   public class SpotifyAnalogAppContext :DbContext
    {
        public SpotifyAnalogAppContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Song> Songs { get; set; }


    }
}
