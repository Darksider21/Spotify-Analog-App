using Moq;
using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.Exceptions;
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
    public class AuthorServiceTests
    {
        public readonly AuthorService _sut;
        public readonly Mock<IAuthorRepository> _authorRepositoryMock = new Mock<IAuthorRepository>();


        public AuthorServiceTests()
        {
            _sut = new AuthorService(_authorRepositoryMock.Object);
        }


        [Fact]
        public async Task GetGetAuthorListAsync_ShouldReturnAllAuthors_When_BothParametersAreNull()
        {


            //Arrange
            var authorsCollection = new List<Author>
            {
                new Author(){Name = "Bob", AuthorId = 1},
                new Author(){Name = "Sam", AuthorId = 2},
                new Author(){Name = "Frodo", AuthorId = 3},
                new Author(){Name = "Mary", AuthorId = 4},
                new Author(){Name = "Duncan", AuthorId = 5},

            };

            _authorRepositoryMock.Setup(x => x.GetAllAuthorsAsync())
                .ReturnsAsync(authorsCollection);

            //Act
            var responce = await _sut.GetAuthorListAsync(null, null);
            //Assert
            AuthorModel[] arrayResponce = responce.ToArray();
            for (int i = 0; i < arrayResponce.Length; i++)
            {
                
                Assert.Equal(authorsCollection[i].AuthorId, arrayResponce[i].AuthorId);
            }

        }

        [Fact]
        public async Task GetGetAuthorListAsync_ShouldReturnAuthorsByNameAndGenre_When_BothParametersAreSet()
        {


            //Arrange
            var authorsCollection = new List<Author>
            {
                new Author(){Name = "Bob", AuthorId = 1}

            };
            var name = "Bob";
            var genre = "Rock";
            _authorRepositoryMock.Setup(x => x.GetByNameAndGenre(name,genre))
                .ReturnsAsync(authorsCollection);

            //Act
            var responce = await _sut.GetAuthorListAsync(name, genre);
            //Assert
            AuthorModel[] arrayResponce = responce.ToArray();
            for (int i = 0; i < arrayResponce.Length; i++)
            {

                Assert.Equal(authorsCollection[i].AuthorId, arrayResponce[i].AuthorId);
            }

        }
        [Fact]
        public async Task GetGetAuthorListAsync_ShouldReturnAuthorsByName_When_OnlyAuthorNameSet()
        {


            //Arrange
            var authorsCollection = new List<Author>
            {
                new Author(){Name = "Bob", AuthorId = 1},
                new Author(){Name = "Dean", AuthorId = 2}

            };
            var name = "Bob";
            string genre = null;
            _authorRepositoryMock.Setup(x => x.GetByNameAsync(name))
                .ReturnsAsync(authorsCollection);

            //Act
            var responce = await _sut.GetAuthorListAsync(name, genre);
            //Assert
            AuthorModel[] arrayResponce = responce.ToArray();
            for (int i = 0; i < arrayResponce.Length; i++)
            {

                Assert.Equal(authorsCollection[i].AuthorId, arrayResponce[i].AuthorId);
            }

        }
        [Fact]
        public async Task GetGetAuthorListAsync_ShouldReturnAuthorsByGenreName_When_OnlyGenreSet()
        {


            //Arrange
            var authorsCollection = new List<Author>
            {
                new Author(){Name = "Bob", AuthorId = 1 , Genre = new Genre() {GenreId = 1, GenreName = "rock" } },
                new Author(){Name = "Sam", AuthorId = 2 , Genre = new Genre() {GenreId = 2, GenreName = "rock" } },
                new Author(){Name = "Robert", AuthorId = 3 , Genre = new Genre() {GenreId = 3, GenreName = "rock" } }

            };
            string name = null;
            string genre = "rock";
            _authorRepositoryMock.Setup(x => x.GetByGenreAsync(genre))
                .ReturnsAsync(authorsCollection);

            //Act
            var responce = await _sut.GetAuthorListAsync(name, genre);
            //Assert
            AuthorModel[] arrayResponce = responce.ToArray();
            for (int i = 0; i < arrayResponce.Length; i++)
            {

                Assert.Equal(authorsCollection[i].AuthorId, arrayResponce[i].AuthorId);
            }

        }

        [Fact]
        public async Task GetGetAuthorListAsync_ShouldThrowContentNotFoundException_When_NoAuthorsReturned()
        {


            //Arrange
            var authorsCollection = new List<Author>();
            string name = "null";
            string genre = "null";
            _authorRepositoryMock.Setup(x => x.GetAllAuthorsAsync())
                .ReturnsAsync(authorsCollection);

            //Act
            
            //Assert
            await Assert.ThrowsAsync<ContentNotFoundException>(async () => await _sut.GetAuthorListAsync(name, genre));

        }


    }
}
