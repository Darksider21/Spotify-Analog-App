using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Models
{
    public class DislikedSong
    {
        public int DislikedSongId { get; set; }

        public Song Song { get; set; }
        public AppUser AppUser { get; set; }

        
    }
}
