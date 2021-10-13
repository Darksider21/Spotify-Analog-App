using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAnalogApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using SpotifyAnalogApp.Data.Data;

namespace SpotifyAnalogApp.Data.Repositiry
{
    public class GenreRepository : Repository<Genre>
    {
        public GenreRepository(SpotifyAnalogAppContext context) : base(context)
        {

        }
    }
}