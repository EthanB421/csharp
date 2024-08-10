using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public interface IUserRepository
    {
        public bool SaveChanges();

        public bool AddEntity<T>(T entityToAdd);

        public bool RemoveEntity<T>(T entityToAdd);
        public IEnumerable<User> GetUsers();
        public User GetSingleUser(int userId);
        public IEnumerable<UserJobInfo> GetUserJobInfo();


    }
}