using SpotifyAnalogApp.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services.ServiceInterfaces
{
    public interface ISongService
    {
        Task<IEnumerable<SongModel>> GetSongList();
        Task<IEnumerable<SongModel>> GetSongsWithAuthorsList();
        Task<SongModel> GetSongById(int songId);

        Task<IEnumerable<SongModel>> GetSongsByAuthorName(string authorName);
        Task<IEnumerable<SongModel>> GetSongsByMultipleAuthors(string[] authorsNames);

        Task<IEnumerable<SongModel>> GetSongsByGenreName(string GenreName);
        Task<IEnumerable<SongModel>> GetSongsByMultipleGenres(string[] GenreNames);

        Task<IEnumerable<SongModel>> GetRandomSongsList(int amountOfSongs);

        Task<IEnumerable<SongModel>> GetRandomSongsListByGenres(int amountOfSongs , string[] genreNames);

        Task<IEnumerable<SongModel>> GetRandomSongsListByAuthors(int amountOfSongs , string[] authorNames);










    }
}
