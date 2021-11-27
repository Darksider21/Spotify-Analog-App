using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Models
{
    public class GenreAnalytics
    {
        public int GenreAnalyticsId { get; set; }
        public int SongsOfThisGenreCount { get; set; }

        
        public Genre Genre { get; set; }
        public AppUser AppUser { get; set; }

    }
}
