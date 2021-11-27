using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpotifyAnalogApp.Data.Models;

namespace SpotifyAnalogApp.Data.Data
{
   public class SpotifyAnalogAppContext : IdentityDbContext
    {
        public SpotifyAnalogAppContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Song> Songs { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<GenreAnalytics> GenreAnalytics { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
