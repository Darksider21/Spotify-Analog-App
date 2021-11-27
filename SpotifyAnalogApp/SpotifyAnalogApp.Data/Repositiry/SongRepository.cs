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
        public async Task<Song> GetSongByIdAsync(int id)
        {

            return await base._dbContext.Songs.Where(x => x.SongId == id)
                .Include(x => x.Author).Include(x => x.Genre).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Song>> GetSongsByIdsAsync(int[] songIds)
        {
            return await _dbContext.Songs.Where(x => songIds.Contains(x.SongId))
                .Include(x => x.Author).Include(x => x.Genre).ToListAsync();
        }

        

        


        public async  Task<IEnumerable<Song>> GetSongsByMultipleGenresAsync(string[] genres)
        {

            return await base._dbContext.Songs.Where(x => genres.Contains(x.Genre.GenreName))
                .Include(x => x.Author).Include(x => x.Genre).ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetSongsByMultipleAuthorsAsync(string[] names)
        {

            return await base._dbContext.Songs.Where(x => names.Contains(x.Author.Name))
                .Include(x => x.Author).Include(x => x.Genre).ToListAsync();

        }

        public async Task<IEnumerable<Song>> GetSongsByGenresAndAuthorsAsync(string[] genres , string[] authors)
        {
            return await base._dbContext.Songs.Where(x => genres.Contains(x.Genre.GenreName) && authors.Contains(x.Author.Name))
                .Include(x => x.Author.Genre).ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetSongWithAuthorsListAsync()
        {
            return await testContext.Songs.Select(x => x).Include(n => n.Author).ToListAsync();
        }


        // Guid.NewGuid() generate random number every execution and EntityFramework know how to handle it unlike Random.next()
        public async Task<IEnumerable<Song>> GetRandomSongsListAsync(int amountOfSongs)
        {
           return await base._dbContext.Songs.OrderBy(_ => Guid.NewGuid()).Take(amountOfSongs)
                .Include(x => x.Author).Include(x => x.Genre).ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetRandomSongsByGenresListAsync(int amountOfSongs, string[] genreNames)
        {
            return await base._dbContext.Songs.Where(x => genreNames.Contains(x.Genre.GenreName)).OrderBy(_ => Guid.NewGuid())
                .Include(x => x.Author).Include(x => x.Genre)
                .Take(amountOfSongs).ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetRandomSongsByAuthorsListAsync(int amountOfSongs, string[] authorNames)
        {
            return await base._dbContext.Songs.Where(x => authorNames.Contains(x.Author.Name)).OrderBy(_ => Guid.NewGuid())
                .Include(x => x.Author).Include(x => x.Genre)
                .Take(amountOfSongs).ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetRandomSongsByAuthorsAndGenresAsync(int amountOfSongs, string[] authorNames, string[] genreNames)
        {
            return await base._dbContext.Songs.Where(x => authorNames.Contains(x.Author.Name) && genreNames.Contains(x.Genre.GenreName)).OrderBy(_ => Guid.NewGuid())
                .Include(x => x.Author).Include(x => x.Genre)
                .Take(amountOfSongs).ToListAsync();
        }
    }
}