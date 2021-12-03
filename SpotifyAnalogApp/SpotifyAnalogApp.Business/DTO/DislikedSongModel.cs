using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO
{
    public class DislikedSongModel
    {
        public int DislikedSongId { get; set; }

        public SongModel Song { get; set; }
        
    }
}
