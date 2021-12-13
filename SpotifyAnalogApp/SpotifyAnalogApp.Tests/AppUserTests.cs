using Moq;
using SpotifyAnalogApp.Business.DTO;
using SpotifyAnalogApp.Business.DTO.RequestDto;
using SpotifyAnalogApp.Business.Exceptions;
using SpotifyAnalogApp.Business.Services;
using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using SpotifyAnalogApp.Data.Models;
using SpotifyAnalogApp.Data.Repositiry.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SpotifyAnalogApp.Tests
{
    public class AppUserTests
    {
        private readonly AppUserService _sut;
        private readonly Mock<IAppUserRepository> _AppUserRepositoryMock = new Mock<IAppUserRepository>();
        private readonly Mock<ISongRepository> _SongRepositoryMock = new Mock<ISongRepository>();
        private readonly Mock<IPlaylistRepository> _PlaylistRepositoryMock = new Mock<IPlaylistRepository>();
        private readonly Mock<IAnalyticsService> _AnalyticsServiceMock = new Mock<IAnalyticsService>();
        private readonly Mock<IDateTimeService> _DateTimeServiceMock = new Mock<IDateTimeService>();

        public AppUserTests()
        {
            _sut = new AppUserService(_AppUserRepositoryMock.Object, _SongRepositoryMock.Object, _PlaylistRepositoryMock.Object, _AnalyticsServiceMock.Object,
                _DateTimeServiceMock.Object);
        }


        [Fact]
        public async Task CreateUserAsync_ShouldReturnUserModel_WhenAllParametersAreSet()
        {

            //Arrange
            var name = "bob";
            var email = "bob@bob.com";
            _AppUserRepositoryMock.Setup(x => x.GetUserByEmail(email)).ReturnsAsync(() => null);

            //Act
            var result = await _sut.CreateUserAsync(name, email);
            //Assert
            Assert.Equal(name, result.Name);
            Assert.Equal(email, result.Email);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldCallCreateUserAsyncOfUserRepository_WhenAllParametersAreSet()
        {

            //Arrange
            var name = "bob";
            var email = "bob@bob.com";
            var date = DateTime.Now;


            _DateTimeServiceMock.Setup(x => x.ReturnDaytimeNow()).Returns(date);
            _AppUserRepositoryMock.Setup(x => x.GetUserByEmail(email)).ReturnsAsync(() => null);



            //Act
            var result = await _sut.CreateUserAsync(name, email);
            //Assert
            _AppUserRepositoryMock.Verify(x => x.CreateUserAsync(It.IsAny<AppUser>()), Times.Exactly(1));


        }
        [Fact]
        public async Task CreateUserAsync_ShouldThrowDuplicateEmailException_WhenDuplicateEmailIsGiven()
        {

            //Arrange
            var name = "bob";
            var email = "bob@bob.com";


            _AppUserRepositoryMock.Setup(x => x.GetUserByEmail(email)).ReturnsAsync(() => new AppUser() { });



            //Act

            //Assert
            await Assert.ThrowsAsync<DuplicateEmailException>(async () => await _sut.CreateUserAsync(name, email));


        }
        [Theory]
        [InlineData("bob", null)]
        [InlineData(null, "bob@kek.com")]
        [InlineData(null, null)]
        public async Task CreateUserAsync_ShouldThrowNullFieldsexception_WhenOneOrMoreParametersAreNull(string name , string email)
        {

            //Arrange
            
            


            _AppUserRepositoryMock.Setup(x => x.GetUserByEmail(email)).ReturnsAsync(() => null);



            //Act

            //Assert
            await Assert.ThrowsAsync<NullFieldsException>(async () => await _sut.CreateUserAsync(name, email));


        }





        [Fact]
        public async Task DeleteUserAsync_ShouldCallDeleteUserAsyncMethod_WhenUserIsFound()
        {

            //Arrange
            _AppUserRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new AppUser());
            //Act
            await _sut.DeleteUserAsync(5);
            //Assert
            _AppUserRepositoryMock.Verify(x => x.DeleteUserAsync(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public async Task DeleteUserAsync_ShouldThrowInvalidUserIdException_WhenUserIsNotFound()
        {

            //Arrange
            _AppUserRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            //Act

            //Assert
            await Assert.ThrowsAsync<InvalidUserIdException>(async () => await _sut.DeleteUserAsync(5));
        }
        [Fact]
        public async Task DeleteUserAsync_ShouldCallGetUserByIdOnce()
        {
            //Arrange
            _AppUserRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new AppUser() { });
            //Act
            await _sut.DeleteUserAsync(5);
            //Assert
            _AppUserRepositoryMock.Verify(x => x.GetUserByIdAsync(It.IsAny<int>()), Times.Once);
        }


        [Fact]
        public async Task GetUserByIdAsync_ShouldCallGetUserByIdAsyncMethod()
        {

            //Arrange
            _AppUserRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(new AppUser());
            //Act
            await _sut.GetUserByIdAsync(5);
            //Assert
            _AppUserRepositoryMock.Verify(x => x.GetUserByIdAsync(It.IsAny<int>()), Times.Once);
        }
        [Fact]
        public async Task GetUserByIdAsync_ShouldThrowInvalidUserIdException_WhenUserIsNotFound()
        {

            //Arrange
            _AppUserRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            //Act

            //Assert
            await Assert.ThrowsAsync<InvalidUserIdException>(async () => await _sut.GetUserByIdAsync(5));
        }
        [Fact]
        public async Task DeleteUserAsync_ShouldReturnUser_WhenUserByUserIdIsFound()
        {
            var user = new AppUser() { AppUserId = 1 };
            //Arrange
            _AppUserRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
            //Act
            var result = await _sut.GetUserByIdAsync(5);
            //Assert
            Assert.Equal(user.AppUserId, result.AppUserId);
        }

        [Fact]
        public async Task UpdateUserInfoAsync_ShouldReturnNewUser_WhenUserModelIsValid()
        {

            //Arrange
            var userRequestModel = new RequestUserModel() { AppUserId = 1, Email = "bob.net@com.net", Name = "Jayson" };
            var oldUser = new AppUser() { Name = "Eliot", Email = "test@kek.net" };

            _AppUserRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(oldUser);
            //Act
            var result = await _sut.UpdateUserInfoAsync(userRequestModel);
            //Assert
            Assert.Equal(userRequestModel.Name, result.Name);
        }
        [Fact]
        public async Task UpdateUserInfoAsync_ShouldThrowInvalidUserIdException_WhenUserNotFound()
        {

            //Arrange
            var userRequestModel = new RequestUserModel() { AppUserId = 1 };
            _AppUserRepositoryMock.Setup(x => x.GetUserByIdAsync(userRequestModel.AppUserId)).ReturnsAsync(() => null);
            //Act
            //Assert
            await Assert.ThrowsAsync<InvalidUserIdException>(async () => await _sut.UpdateUserInfoAsync(userRequestModel));
        }



    }
}
