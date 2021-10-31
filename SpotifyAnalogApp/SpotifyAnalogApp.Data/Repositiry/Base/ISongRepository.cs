using SpotifyAnalogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Data.Repositiry.Base
{
    public interface ISongRepository
    {
        public Task<IEnumerable<Song>> GetSongListAsync();
        public Task<IEnumerable<Song>> GetSongWithAuthorsListAsync();
    }
}
