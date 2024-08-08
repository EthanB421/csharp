using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public class UserRepository : IUserRepository
    {
    DataContextEF _ef;
    public UserRepository(IConfiguration config)
    {
        _ef = new DataContextEF(config);
    }

    public bool SaveChanges()
    {
        return _ef.SaveChanges()>0;
    }

    // public void AddEntity<T>(T entityToAdd)

    public bool AddEntity<T>(T entityToAdd)
    {
        if(entityToAdd!=null)
        {
        _ef.Add(entityToAdd);
        return true;
        }
        return false;
    }

        public bool RemoveEntity<T>(T entityToAdd)
    {
        if(entityToAdd!=null)
        {
        _ef.Remove(entityToAdd);
        return true;
        }
        return false;
    }

    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _ef.Users.ToList<User>();
        return users;
    }

    public IEnumerable<UserJobInfo> GetUserJobInfo()
    {
        IEnumerable<UserJobInfo> usersInfo = _ef.UserJobInfo.ToList<UserJobInfo>();
        return usersInfo;
    }

    public User GetSingleUser(int userId)
    {

    User? user = _ef.Users.Where(u => u.UserId == userId).FirstOrDefault<User>();
    if (user != null)
    {
    return user;        
    }
    throw new Exception("User doesn't exist");
    }

    }
}