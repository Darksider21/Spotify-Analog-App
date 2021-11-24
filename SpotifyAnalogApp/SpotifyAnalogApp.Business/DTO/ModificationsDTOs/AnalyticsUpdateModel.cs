using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO.ModificationsDTOs
{
    public class AnalyticsUpdateModel
    {
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
