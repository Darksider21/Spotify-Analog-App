using AutoMapper;
using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.Exceptions;
using SpotifyAnalogApp.Business.Mapper;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using SpotifyAnalogApp.Data.Models;
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

        public async Task<SongModel> GetSongByIdAsync(int songId)
        {
            var song = await _songRepository.GetSongByIdAsync(songId);
            if (song == null)
            {
                throw new BaseCustomException(404, "Invalid Song Id");
            }

            return ObjectMapper.Mapper.Map<SongModel>(song);
            
        }
        public async Task<IEnumerable<SongModel>> GetSongsWithAuthorsListAsync()
        {
            var songsWithAuthorsList = await _songRepository.GetSongWithAuthorsListAsync();

            

            return ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsWithAuthorsList);

        }
        public async Task<IEnumerable<SongModel>> GetSongListAsync()
        {
            var songsList = await _songRepository.GetSongListAsync();

            if (!songsList.Any())
            {
                throw new BaseCustomException(402, "Unable to fetch songs");
            }
            return ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsList);
        }

        
        public async Task<IEnumerable<SongModel>> GetSongsByAuthorsAndGenresAsync(AuthorGenreDTO dto)
        {
            IEnumerable<Song> songsList = new List<Song>(); 
            if (dto.Authors != null && dto.Genres != null)
            {
                 songsList = await _songRepository.GetSongsByGenresAndAuthorsAsync(dto.Genres, dto.Authors);
            }
            else if (dto.Authors != null)
            {
                songsList = await _songRepository.GetSongsByMultipleAuthorsAsync(dto.Authors);
            }
            else
            {
                songsList = await _songRepository.GetSongsByMultipleGenresAsync(dto.Genres);
            }
            return ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsList);
        }

        public async Task<IEnumerable<SongModel>> GetRandomSongsByAuthorsAndGenresAsync(int amountOfSongs, AuthorGenreDTO dto)
        {
            IEnumerable<Song> songsList = new List<Song>();

            if (dto.Authors != null && dto.Genres != null)
            {
                songsList = await _songRepository.GetRandomSongsByAuthorsAndGenresAsync(amountOfSongs, dto.Authors,dto.Genres);
            }
            else if (dto.Authors != null)
            {
                songsList = await _songRepository.GetRandomSongsByAuthorsListAsync(amountOfSongs,dto.Authors);
            }
            else if(dto.Genres != null)
            {
                songsList = await _songRepository.GetRandomSongsByGenresListAsync(amountOfSongs, dto.Genres);
            }
            else
            {
                songsList = await _songRepository.GetRandomSongsListAsync(amountOfSongs);
            }

            return ObjectMapper.Mapper.Map<IEnumerable<SongModel>>(songsList);
        }
    }
}
