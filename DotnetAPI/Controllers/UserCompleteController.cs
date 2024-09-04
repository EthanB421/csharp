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
    IUserControllerService userService;
    public UserCompleteController(IUserControllerService userService)
    {
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
        this.userService.UpsertUser(user);
        return Ok();
        
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        this.userService.DeleteUser(userId);
        return Ok(); // HTTP 200 OK
    }
}