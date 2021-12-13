using Moq;
using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.Services;
using SpotifyAnalogApp.Data.Models;
using SpotifyAnalogApp.Data.Repositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SpotifyAnalogApp.Tests
{
    public class GenreServiceTests
    {

        public readonly GenreService _sut;
        private readonly Mock<IGenreRepository> _genreRepositoryMock = new Mock<IGenreRepository>();


        public GenreServiceTests()
        {
            _sut = new GenreService(_genreRepositoryMock.Object);
        }

        [Fact]
        public async Task GetGenresListAsync_ShouldReturnGenresCollection()
        {
            //Arrange
           
            var genresCollection = new List<Genre>()
            {
                new Genre() { GenreId = 1, GenreName = "Rock" },
                new Genre() { GenreId = 2, GenreName = "Metal" }
            };
            _genreRepositoryMock.Setup(x => x.GetAllGenresAsync())
                .ReturnsAsync(genresCollection);

            //Act
            var responce = await _sut.GetGenreListAsync();


            //Assert
            GenreModel[] responceArray = responce.ToArray();
            for (int i = 0; i < responceArray.Length; i++)
            {
                Assert.Equal(genresCollection[i].GenreId ,responceArray[i].GenreId);
                
            }
        }
    }
}
