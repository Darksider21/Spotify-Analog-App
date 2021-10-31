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
    public class GenreService : IGenreService
    {

        private IGenreRepository genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreModel>> GetGenreByNameList(string genre)
        {
            var genreList = await genreRepository.GetGenreByNameList(genre);

            var mapped = ObjectMapper.Mapper.Map<IEnumerable<GenreModel>>(genreList);

            return mapped;
        }

        public async Task<IEnumerable<GenreModel>> GetGenreList()
        {
            var genreList = await genreRepository.GetAllGenresAsync();


            var mapped = ObjectMapper.Mapper.Map<IEnumerable<GenreModel>>(genreList);
            return mapped;
        }
    }
}
