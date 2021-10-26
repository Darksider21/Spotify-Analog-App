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
    public class SongRepository : Repository<Song>, ISongRepository
    {
        private SpotifyAnalogAppContext testContext;
        public SongRepository(SpotifyAnalogAppContext context) : base(context)
        {
            testContext = context;

        }

        public async Task<IEnumerable<Song>> GetSongListAsync()
        {
            return await GetAllAsync();
        }
    }
}