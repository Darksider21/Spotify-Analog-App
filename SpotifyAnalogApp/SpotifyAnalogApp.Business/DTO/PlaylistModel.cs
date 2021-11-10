using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO
{
    public class PlaylistModel
    {
        public int PlaylistId { get; set; }

        public string PlaylistName { get; set; }

        public ICollection<SongModel> SongsInPlaylist { get; set; }

       
    }
}
