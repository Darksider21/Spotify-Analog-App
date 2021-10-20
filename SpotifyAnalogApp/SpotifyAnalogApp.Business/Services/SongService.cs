using AutoMapper;
using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.Mapper;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using SpotifyAnalogApp.Data.Repositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services
{
    public class SongService : ISongService
    {

        private readonly ISongRepository _songRepository;


        public SongService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public Task<SongModel> GetSongById(int songId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SongModel>> GetSongList()
        {
            var songsList = await _songRepository.GetSongListAsync();


            var mapped = ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsList);
            return mapped;
        }

        public Task<IEnumerable<SongModel>> GetSongsByAuthorName(string authorName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SongModel>> GetSongsByGenreName(string GenreName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SongModel>> GetSongsByMultipleAuthors(string[] authorsNames)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SongModel>> GetSongsByMultipleGenres(string[] GenreNames)
        {
            throw new NotImplementedException();
        }
    }
}
