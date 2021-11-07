using SpotifyAnalogApp.Business.DTO;
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
    public class UserService : IUserService
    {

        private IUserRepository userRepository;

        public UserService(IUserRepository repository)
        {
            userRepository = repository;
        }
        public  async  Task<UserModel> CreateUser(string name, string Email)
        {
            var now = DateTime.Now;
            var user = new User { Name = name, Email = Email, DateCreated = now };
           await  userRepository.CreateUser(user);
            var mapped = ObjectMapper.Mapper.Map<UserModel>(user);
            return mapped;
        }

        public async Task DeleteUser(int userId)
        {
            await userRepository.DeleteUser(userId);
        }

        public  async Task<UserModel> GetUserById(int userId)
        {
            var user = await userRepository.GetUserById(userId);
            var mapped = ObjectMapper.Mapper.Map<UserModel>(user);
            return mapped;
        }

        public async  Task<ICollection<UserModel>> GetUsers()
        {
            var users = await userRepository.GetUsersListAsync();
            var mapped = ObjectMapper.Mapper.Map<ICollection<UserModel>>(users);
            return mapped;
        }

        public  async Task<UserModel> ModifyFavorites(string action, int userId, int[] songsIds)
        {
            throw new NotImplementedException();
        }

        public async  Task<UserModel> UpdateUserInfo(string name, string Email, int userId)
        {
            var currentuser = await userRepository.GetUserById(userId);

            ModifyUserModel model = new ModifyUserModel { Name = name, Email = Email };

            var newUser = ObjectMapper.Mapper.Map<ModifyUserModel, User>(model, currentuser);

            await userRepository.UpdateUser(newUser);
            var mapped = ObjectMapper.Mapper.Map<UserModel>(newUser);
            return mapped;
        }
    }
}
