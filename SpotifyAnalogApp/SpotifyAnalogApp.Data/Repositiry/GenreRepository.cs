using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyAnalogApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using SpotifyAnalogApp.Data.Data;
using SpotifyAnalogApp.Data.Repositiry.Base;

namespace SpotifyAnalogApp.Data.Repositiry
{
    public class GenreRepository : Repository<Genre> ,IGenreRepository
    {
        public GenreRepository(SpotifyAnalogAppContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int genreId)
        {
            return await GetByIdAsync(genreId);
        }

        public async Task<Genre> GetGenreByNameListAsync(string name)
        {
            return await _dbContext.Genres.Where(x => x.GenreName.Equals(name)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Genre>> GetGenresByIdsAsync(int[] genreIds)
        {
            return await _dbContext.Genres.Where(x => genreIds.Contains(x.GenreId)).ToListAsync();
        }
    }
}