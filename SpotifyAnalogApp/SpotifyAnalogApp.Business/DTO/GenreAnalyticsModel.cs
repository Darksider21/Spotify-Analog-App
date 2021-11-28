using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO
{
    public class GenreAnalyticsModel
    {
        public int GenreAnalyticsId { get; set; }
        public int SongsOfThisGenreCount { get; set; }
        public GenreModel Genre { get; set; }
    }
}
