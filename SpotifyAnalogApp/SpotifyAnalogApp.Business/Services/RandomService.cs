using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services
{
    public class RandomService : IRandomService
    {
        public Random Random { get; set; }

        public RandomService()
        {
            Random = new Random();
        }
        public Random GetRandom()
        {
            return Random;
        }
    }
}
