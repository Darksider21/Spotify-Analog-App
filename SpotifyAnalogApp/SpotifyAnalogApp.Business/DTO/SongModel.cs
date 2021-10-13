﻿using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO
{
    public class SongModel
    {
        public int SongId { get; protected set; }
        

        public string Name { get; set; }

        public Author Author { get; set; }

    }
}
