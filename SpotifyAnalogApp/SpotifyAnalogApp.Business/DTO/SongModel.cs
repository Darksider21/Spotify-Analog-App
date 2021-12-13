using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO
{
    public class SongModel
    {
        public int SongId { get; set; }
        

        public string Name { get; set; }

        public AuthorInSongModelModel Author { get; set; }
        public Genre Genre { get; set; }

    }
}
