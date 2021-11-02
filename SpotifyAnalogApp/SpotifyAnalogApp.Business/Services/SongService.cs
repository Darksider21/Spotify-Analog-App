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

        public async Task<SongModel> GetSongById(int songId)
        {
            var songsWithAuthorsList = await _songRepository.GetSongById(songId);

            var mapped = ObjectMapper.Mapper.Map<SongModel>(songsWithAuthorsList);
            return mapped;
        }
        public async Task<IEnumerable<SongModel>> GetSongsWithAuthorsList()
        {
            var songsWithAuthorsList = await _songRepository.GetSongWithAuthorsListAsync();

            var mapped = ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsWithAuthorsList);
            return mapped;

        }
        public async Task<IEnumerable<SongModel>> GetSongList()
        {
            var songsList = await _songRepository.GetSongListAsync();


            var mapped = ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsList);
            return mapped;
        }

        public async Task<IEnumerable<SongModel>> GetSongsByAuthorName(string authorName)
        {
            var songsList = await _songRepository.GetSongsByAuthorName(authorName);


            var mapped = ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsList);
            return mapped;
        }

        public async Task<IEnumerable<SongModel>> GetSongsByGenreName(string GenreName)
        {
            var songsList = await _songRepository.GetSongsByGenreName(GenreName);


            var mapped = ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsList);
            return mapped;
        }

        public async Task<IEnumerable<SongModel>> GetSongsByMultipleAuthors(string[] authorsNames)
        {
            var songsList = await _songRepository.GetSongsByMultipleAuthors(authorsNames);


            var mapped = ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsList);
            return mapped;
        }

        public async Task<IEnumerable<SongModel>> GetSongsByMultipleGenres(string[] GenreNames)
        {
            var songsList = await _songRepository.GetSongsByMultipleGenres(GenreNames);


            var mapped = ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsList);
            return mapped;
        }

        public async Task<IEnumerable<SongModel>> GetRandomSongsList(int amountOfSongs)
        {
            var songsList = await _songRepository.GetRandomSongsListAsync(amountOfSongs);


            var mapped = ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsList);
            return mapped;
        }

        public async Task<IEnumerable<SongModel>> GetRandomSongsListByGenres(int amountOfSongs, string[] genreNames)
        {
            var songsList = await _songRepository.GetRandomSongsByGenresListAsync(amountOfSongs , genreNames);


            var mapped = ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsList);
            return mapped;
        }

        public async Task<IEnumerable<SongModel>> GetRandomSongsListByAuthors(int amountOfSongs, string[] authorNames)
        {
            var songsList = await _songRepository.GetRandomSongsByAuthorsListAsync(amountOfSongs, authorNames);


            var mapped = ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsList);
            return mapped;
        }
    }
}
