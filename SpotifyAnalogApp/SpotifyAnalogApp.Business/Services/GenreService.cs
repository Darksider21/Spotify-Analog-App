using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services
{
    public class GenreService : IGenreService
    {
        public Task<IEnumerable<GenreModel>> GetGenreList()
        {
            throw new NotImplementedException();
        }
    }
}
