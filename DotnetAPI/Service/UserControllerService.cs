using DotnetAPI.Interfaces;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Service
{
    public class UserControllerService : IUserControllerService
    {

        private readonly IUserControllerRepository userRepository;

        public UserControllerService(IUserControllerRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public bool DeleteUser(int userId)
        {
            return this.userRepository.DeleteUser(userId);
        }

        public IEnumerable<UserComplete> GetUsers(int userId, bool isActive)
        {
            return this.userRepository.GetUsers(userId, isActive);
        }

        public DateTime TestConnection()
        {
            return this.userRepository.TestConnection();
        }

        public bool UpsertUser(UserComplete user)
        {
            return this.userRepository.UpsertUser(user);
        }
    }
}