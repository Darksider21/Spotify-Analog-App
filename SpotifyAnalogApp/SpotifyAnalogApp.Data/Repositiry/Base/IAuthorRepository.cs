using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Repositiry.Base
{
    public interface IAuthorRepository
    {
        public Task<IEnumerable<Author>> GetAllAuthorsAsync();
        public Task<IEnumerable<Author>> GetByNameAsync(string name);
        public Task<IEnumerable<Author>> GetByGenreAsync(string genre);

    }
}
