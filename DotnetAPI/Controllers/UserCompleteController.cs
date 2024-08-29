using DotnetAPI.Models;
using DotnetAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;
using DotnetAPI.Service;
using DotnetAPI.Interfaces;

namespace DotnetAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserCompleteController : ControllerBase
{
    DataContextDapper _dapper;
    IUserControllerService userService;
    public UserCompleteController(IConfiguration config, IUserControllerService userService)
    {
        _dapper = new DataContextDapper(config);
        this.userService = userService;
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return userService.TestConnection();
    }


    [HttpGet("GetUsers/{userId}/{isActive}")]
 
    public IEnumerable<UserComplete> GetUsers(int userId, bool isActive)
    {
        return userService.GetUsers(userId, isActive);
    }


    [HttpPut("UpsertUser")]
    public IActionResult UpsertUser(UserComplete user)
    {
        bool completed = userService.UpsertUser(user);
        if(completed)
        {
        return Ok();
        }
        throw new Exception("Failed to Update User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        bool isDeleted = userService.DeleteUser(userId);
        if (isDeleted)
        {
            return Ok(); // HTTP 200 OK
        }throw new Exception("Failed to delete user");
        
    }
}