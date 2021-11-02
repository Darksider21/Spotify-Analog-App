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
        public async Task<Song> GetSongById(int id)
        {
            return await base._dbContext.Songs.Where(x => x.SongId == id).Include(x => x.Author.Genre).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Song>> GetSongsByAuthorName(string name)
        {
            return await GetAsync(x => x.Author.Name.ToLower().Contains(name));
        }

        public async Task<IEnumerable<Song>> GetSongsByGenreName(string genre)
        {
            return await GetAsync(x => x.Genre.GenreName.ToLower().Contains(genre));
        }


        public async  Task<IEnumerable<Song>> GetSongsByMultipleGenres(string[] genres)
        {

            return await base._dbContext.Songs.Where(x => genres.Contains(x.Genre.GenreName)).ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetSongsByMultipleAuthors(string[] names)
        {

            return await base._dbContext.Songs.Where(x => names.Contains(x.Author.Name)).ToListAsync();

        }

        public async Task<IEnumerable<Song>> GetSongWithAuthorsListAsync()
        {
            return await testContext.Songs.Select(x => x).Include(n => n.Author).ToListAsync();
        }


        // Guid.NewGuid() generate random number every execution and EntityFramework know how to handle it
        public async Task<IEnumerable<Song>> GetRandomSongsListAsync(int amountOfSongs)
        {
            return await base._dbContext.Songs.OrderBy(_ => Guid.NewGuid() ).Take(amountOfSongs).ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetRandomSongsByGenresListAsync(int amountOfSongs, string[] genreNames)
        {
            return await base._dbContext.Songs.Where(x => genreNames.Contains(x.Genre.GenreName)).OrderBy(_ => Guid.NewGuid()).Take(amountOfSongs).ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetRandomSongsByAuthorsListAsync(int amountOfSongs, string[] authorNames)
        {
            return await base._dbContext.Songs.Where(x => authorNames.Contains(x.Author.Name)).OrderBy(_ => Guid.NewGuid()).Take(amountOfSongs).ToListAsync();
        }
    }
}