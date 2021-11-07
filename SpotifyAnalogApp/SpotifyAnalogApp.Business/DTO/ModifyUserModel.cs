using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO
{
    public class ModifyUserModel
    {
        public int UserId { get; set; }


        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public string Email { get; set; }

        public ICollection<Song> FavoriteSongs { get; set; }

        public ICollection<Playlist> UsersPlaylists { get; set; }
    }
}
