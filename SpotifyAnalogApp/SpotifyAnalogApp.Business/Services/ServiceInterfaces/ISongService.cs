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
        Task<IEnumerable<SongModel>> GetSongListAsync();
        Task<IEnumerable<SongModel>> GetSongsWithAuthorsListAsync();
        Task<SongModel> GetSongByIdAsync(int songId);

        Task<IEnumerable<SongModel>> GetSongsByAuthorsAndGenresAsync(AuthorGenreDTO dto);

        Task<IEnumerable<SongModel>> GetRandomSongsByAuthorsAndGenresAsync(int amountOfSongs, AuthorGenreDTO dto);










    }
}
