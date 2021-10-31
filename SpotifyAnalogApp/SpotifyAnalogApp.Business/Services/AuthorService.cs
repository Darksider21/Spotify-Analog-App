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
    public class AuthorService : IAuthorService
    {

        private IAuthorRepository authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorModel>> GetAuthorByNameList(string name)
        {
            var authorList = await authorRepository.GetByNameAsync(name);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<AuthorModel>>(authorList);
            
            return mapped;
        }

        public async Task<IEnumerable<AuthorModel>> GetAuthorList()
        {
            var authorList = await authorRepository.GetAllAuthorsAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<AuthorModel>>(authorList);
            return mapped;
        }

        
    }
}
