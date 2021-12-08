using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO
{
    public class WeightedSong
    {
        public Song Song { get; set; }
        public double Weight { get; set; }
    }
}
