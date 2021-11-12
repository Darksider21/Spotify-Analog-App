using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Repositiry.Base
{
    public interface IGenreRepository
    {
        public Task<IEnumerable<Genre>> GetAllGenresAsync();

        public Task<IEnumerable<Genre>> GetGenreByNameList(string name);

        

    }
}
