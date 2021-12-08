using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO
{
    public class GenreProportion
    {
        public Genre Genre { get; set; }
        public double Percentage { get; set; }
    }
}
