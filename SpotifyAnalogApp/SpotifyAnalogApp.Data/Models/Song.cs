using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Models
{
     public class Song
    {
        public int SongId { get; set; }
        [Required]

        public string Name { get; set; }

        public Author Author { get; set; }
        public Genre Genre { get; set; }
       


        public ICollection<Playlist> Playlists { get; set; }

        public ICollection<AppUser> Users { get; set; }

        public ICollection<DislikedSong> DislikedSongs { get; set; }
    }
}
