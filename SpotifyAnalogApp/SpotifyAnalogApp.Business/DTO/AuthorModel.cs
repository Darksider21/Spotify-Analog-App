﻿using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO
{
    public class AuthorModel
    {
        public class Author
        {
            public int AuthorId { get; protected set; }
            
            public string Name { get; set; }

            public ICollection<Song> Songs { get; set; }
            public Genre Genre { get; set; }
        }
    }
}
