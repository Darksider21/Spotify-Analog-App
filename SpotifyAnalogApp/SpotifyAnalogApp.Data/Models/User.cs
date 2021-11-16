using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Analytics Analytics { get; set; }
        public ICollection<Song> FavoriteSongs { get; set; }

        public ICollection<Playlist> UsersPlaylists { get; set; }

    }
}
