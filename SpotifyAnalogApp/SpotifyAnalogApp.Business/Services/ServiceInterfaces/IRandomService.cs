﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services.ServiceInterfaces
{
    public interface IRandomService
    {
        public Random Random { get; set; }

        public Random GetRandom();
    }
}
