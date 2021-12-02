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
    public class AuthorService : IAuthorService
    {

        private IAuthorRepository authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

       

        public async Task<IEnumerable<AuthorModel>> GetAuthorListAsync(string name , string genre)
        {
            IEnumerable<Author> authorList = new List<Author>();
            if(name != null && genre != null)
            {
                authorList = await authorRepository.GetByNameAndGenre(name, genre);
            }
            else if (name != null)
            {
                authorList = await authorRepository.GetByNameAsync(name);
            }
            else if (genre!= null)
            {
                authorList = await authorRepository.GetByGenreAsync(genre);
            }
            else
            {
                 authorList = await authorRepository.GetAllAuthorsAsync();
            }

            if (authorList.Any())
            {
                throw new ContentNotFoundException();
            }
            
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<AuthorModel>>(authorList);
            return mapped;
        }
        


    }
}
