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
        public Task<Song> GetSongById(int id);
        public Task<IEnumerable<Song>> GetSongsByIds(int[] songIds);

        public Task<IEnumerable<Song>> GetSongsByMultipleAuthors(string[] names);
        public Task<IEnumerable<Song>> GetSongsByMultipleGenres(string[] genres);
        public Task<IEnumerable<Song>> GetSongsByGenresAndAuthors(string[] genres, string[] authors);

        public Task<IEnumerable<Song>> GetRandomSongsListAsync(int amountOfSongs);

        public Task<IEnumerable<Song>> GetRandomSongsByGenresListAsync(int amountOfSongs , string[] genreNames);

        public Task<IEnumerable<Song>> GetRandomSongsByAuthorsListAsync(int amountOfSongs, string[] authorNames);

        public Task<IEnumerable<Song>> GetRandomSongsByAuthorsAndGenresAsync(int amountOfSongs, string[] authorNames, string[] genreNames);




    }
}
