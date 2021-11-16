using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Models
{
    public class Analytics
    {
        public int AnalyticsId { get; set; }

        public int TotalSongsCount { get; set; }

        public int MetalSongsCount { get; set; }
        public int JPopSongsCount { get; set; }
        public int RockSongsCount { get; set; }
        public int ClassicalSongsCount { get; set; }
        public int ElectronicSongsCount { get; set; }
        public int PopSongsCount { get; set; }
        public int JazzSongsCount { get; set; }

    }
}
