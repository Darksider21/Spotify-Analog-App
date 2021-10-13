using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.DTO
{
    class GenreModel
    {
        public int GenreId { get; protected set; }

        public string GenreName { get; set; }


        public ICollection<Author> Authors { get; set; }
    }
}
