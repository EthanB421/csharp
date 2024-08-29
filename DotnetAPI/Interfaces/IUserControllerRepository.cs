using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Interfaces
{
    public interface IUserControllerRepository{
    public IEnumerable<UserComplete> GetUsers(int userId, bool isActive);
    public bool UpsertUser(UserComplete user);
    public bool DeleteUser(int userId);
    public DateTime TestConnection();
    }
}