using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Models
{
    public class AppUser
    {
        public int AppUserId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<GenreAnalytics> Analytics { get; set; }
        public ICollection<Song> FavoriteSongs { get; set; }
        public ICollection<DislikedSong> DislikedSongs { get; set; }

        public ICollection<Playlist> UsersPlaylists { get; set; }

    }
}
