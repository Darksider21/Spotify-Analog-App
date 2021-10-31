using SpotifyAnalogApp.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services.ServiceInterfaces
{
   public interface IAuthorService
    {
        public Task<IEnumerable<AuthorModel>> GetAuthorList();

        public Task<IEnumerable<AuthorModel>> GetAuthorByNameList(string name);
        public Task<IEnumerable<AuthorModel>> GetAuthorByGenreList(string genre);


    }
}
