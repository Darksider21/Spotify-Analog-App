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
    public class AuthorRepository : Repository<Author> , IAuthorRepository
    {
       public AuthorRepository(SpotifyAnalogAppContext context) :base(context)
        {

           

        }


        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await GetAllAsync();
        }

        public async Task<IEnumerable<Author>> GetByNameAsync(string name)
        {
            return await base._dbContext.Authors.Where(x => x.Name.ToLower().Contains(name.ToLower())).Include(x => x.Genre).ToListAsync();
        }
    }
}
