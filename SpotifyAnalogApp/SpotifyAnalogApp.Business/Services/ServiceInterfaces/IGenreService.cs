using SpotifyAnalogApp.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services.ServiceInterfaces
{
    public interface IGenreService
    {
      public  Task<IEnumerable<GenreModel>> GetGenreList();

      public  Task<IEnumerable<GenreModel>> GetGenreByNameList(string genre);


    }


}
