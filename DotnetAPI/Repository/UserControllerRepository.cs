using System.Data;
using Dapper;
using DotnetAPI.Interfaces;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Service
{
    public class UserControllerRepository : IUserControllerRepository
    {
        private readonly DataContextDapper _dapper;
        public UserControllerRepository(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        public bool DeleteUser(int userId)
        {
            string sql = @"EXEC TutorialAppSchema.spUser_Delete @userId= @UserIdParameter";
            DynamicParameters sqlParameters = new DynamicParameters();
            sqlParameters.Add("@UserIdParameter", userId,DbType.Int32);
            return _dapper.ExecuteSqlWithParameters(sql, sqlParameters);
        }

        public IEnumerable<UserComplete> GetUsers(int userId, bool isActive)
        {
            string sql = @"EXEC TutorialAppSchema.spUsers_Get";
            string parameters = "";
            DynamicParameters sqlParameters = new DynamicParameters();
            
            if(userId != 0)
            {
                sql += " @UserId=@UserIdParameter";
                sqlParameters.Add("@UserIdParameter", userId, DbType.Int32);
            }        
            if(isActive)
            {
                sql += ", @Active=@ActiveParameter" +isActive.ToString();
                sqlParameters.Add("@ActiveParameter", isActive, DbType.Boolean);
            }
            if(parameters.Length > 0)
            {
                sql += parameters.Substring(1);
            }
            
            IEnumerable<UserComplete> users = _dapper.LoadDataWithParam<UserComplete>(sql, sqlParameters);
            return users;
        }

        public DateTime TestConnection()
        {
            return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
        }

        public bool UpsertUser(UserComplete user)
        {
            string sql = @"EXEC TutorialAppSchema.spUser_Upsert
                    @FirstName = @FirstNameParameter,
                    @LastName= @LastNameParameter,
                    @Email= @EmailParameter,
                    @Gender= @GenderParameter,
                    @Active= @ActiveParameter,
                    @JobTitle= @JobTitleParameter,
                    @Department= @DepartmentParameter,
                    @Salary= @SalaryParameter,
                    @UserId = @UserIdParameter";
            DynamicParameters sqlParameters = new DynamicParameters();
            sqlParameters.Add("@FirstNameParameter", user.FirstName, DbType.String);
            sqlParameters.Add("@LastNameParameter", user.LastName, DbType.String);
            sqlParameters.Add("@EmailParameter", user.Email, DbType.String);
            sqlParameters.Add("@GenderParameter", user.Gender, DbType.String);
            sqlParameters.Add("@ActiveParameter", user.Active, DbType.Boolean);
            sqlParameters.Add("@JobTitleParameter", user.JobTitle, DbType.String);
            sqlParameters.Add("@DepartmentParameter", user.Department, DbType.String);
            sqlParameters.Add("@SalaryParameter", user.Salary, DbType.Decimal);
            sqlParameters.Add("@UserIdParameter", user.UserId, DbType.Int32);
            return _dapper.ExecuteSqlWithParameters(sql, sqlParameters);
        }        
    }
}