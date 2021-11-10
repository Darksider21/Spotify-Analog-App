using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO
{
    public class UserModel
    {
        public int UserId { get; set; }

       
        public string Name { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public string Email { get; set; }

        public ICollection<SongModel> FavoriteSongs { get; set; }

        public ICollection<PlaylistModel> UsersPlaylists { get; set; }
    }
}
