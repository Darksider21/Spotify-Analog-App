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

        Task<IEnumerable<SongModel>> GetSongsByAuthorsAndGenres(AuthorGenreDTO dto);

        Task<IEnumerable<SongModel>> GetRandomSongsByAuthorsAndGenres(int amountOfSongs, AuthorGenreDTO dto);










    }
}
