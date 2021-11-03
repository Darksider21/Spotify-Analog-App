using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Models
{
    public class Playlist
    {
        public int PlaylistId { get; set; }

        public string PlaylistName { get; set; }

        public ICollection<Song> SongsInPlaylist { get; set; }

        public User User { get; set; }
    }
}
