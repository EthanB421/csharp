using DotnetAPI.Models;
using DotnetAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using DotnetAPI.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using AutoMapper;

namespace DotnetAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserControllerEF : ControllerBase
{
    DataContextEF _ef;
    IMapper _mapper;
    public UserControllerEF(IConfiguration config)
    {
        _ef = new DataContextEF(config);
        _mapper = new Mapper(new MapperConfiguration(cfg=>{
            cfg.CreateMap<UserToAddDto, User>();
        }));
    }


    [HttpGet("GetUsersEF")]
 
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _ef.Users.ToList<User>();
        return users;
    }

    [HttpGet("GetUserEF/{userId}")]
    public User GetSingleUser(int userId)
    {

    User? user = _ef.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();
    if (user != null)
    {
    return user;        
    }
    throw new Exception("User doesn't exist");
    }

    [HttpPut("EditUserEF")]
    public IActionResult EditUser(User user)
    {
        User? userEdit = _ef.Users
        .Where(u => u.UserId == user.UserId)
        .FirstOrDefault<User>();

        if(userEdit != null)
        {
        userEdit.FirstName = user.FirstName ;
          userEdit.LastName = user.LastName;
          userEdit.Email=  user.Email;
          userEdit.Gender = user.Gender;
          userEdit.Active = user.Active;
          if(_ef.SaveChanges()>0)
          {
            return Ok();
          }
        }
        throw new Exception("Failed to Update User");
    }

    [HttpPost("AddUserEF")]
        public IActionResult AddUser(UserToAddDto user)
    {
        User newUser = _mapper.Map<User>(user);
          _ef.Add(newUser);
        if(_ef.SaveChanges()>0)
        {
        return Ok();
        }
        throw new Exception("Failed to Add User");
    }

    [HttpDelete("DeleteUserEF/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User? userDelete = _ef.Users
        .Where(u => u.UserId == userId)
        .FirstOrDefault<User>();

        if(userDelete != null)
        {
        _ef.Remove(userDelete);
            if(_ef.SaveChanges()>0)
                {
                return Ok();
                }
        }

        throw new Exception("Failed to Delete User");
    }

    [HttpGet("GetUserJobInfo")]
 
    public IEnumerable<UserJobInfo> GetUserJobInfo()
    {
        IEnumerable<UserJobInfo> users = _ef.UserJobInfo.ToList<UserJobInfo>();
        return users;
    }
}