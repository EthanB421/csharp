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
    IUserRepository _userRepo;
    IMapper _mapper;
    public UserControllerEF(IConfiguration config, IUserRepository userRepository)
    {
        _userRepo = userRepository;
        _mapper = new Mapper(new MapperConfiguration(cfg=>{
            cfg.CreateMap<UserToAddDto, User>();
        }));
    }


    [HttpGet("GetUsersEF")]
 
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _userRepo.GetUsers();
        return users;
    }

    [HttpGet("GetUserEF/{userId}")]
    public User GetSingleUser(int userId)
    {
        return _userRepo.GetSingleUser(userId);
    }

    [HttpPut("EditUserEF")]
    public IActionResult EditUser(User user)
    {
        User? userEdit = _userRepo.GetSingleUser(user.UserId);
        if(userEdit != null)
        {
        userEdit.FirstName = user.FirstName ;
          userEdit.LastName = user.LastName;
          userEdit.Email=  user.Email;
          userEdit.Gender = user.Gender;
          userEdit.Active = user.Active;
          if(_userRepo.SaveChanges())
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
        _userRepo.AddEntity<User>(newUser);
        if(_userRepo.SaveChanges())
        {
        return Ok();
        }
        throw new Exception("Failed to Add User");
    }

    [HttpDelete("DeleteUserEF/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User? userDelete = _userRepo.GetSingleUser(userId);

        if(userDelete != null)
        {
        _userRepo.RemoveEntity<User>(userDelete);
            if(_userRepo.SaveChanges())
                {
                return Ok();
                }
        }

        throw new Exception("Failed to Delete User");
    }

    [HttpGet("GetUserJobInfo")]
 
    public IEnumerable<UserJobInfo> GetUserJobInfo()
    {
        IEnumerable<UserJobInfo> users = _userRepo.GetUserJobInfo();
        return users;
    }
}