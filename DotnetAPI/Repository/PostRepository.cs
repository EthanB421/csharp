using System.Data;
using Dapper;
using DotnetAPI.Data;
using DotnetAPI.Interfaces;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContextDapper _dapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _dapper = new DataContextDapper(config);
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetMyPosts()
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Get @UserId = @UserIdParameter";
            DynamicParameters sqlParameters = new DynamicParameters();

            // Get the UserId from the HttpContext
            var userId =_httpContextAccessor.HttpContext?.User.FindFirst("userId")?.Value;

            // Convert userId to an integer and handle cases where userId might be null
            if (int.TryParse(userId, out int parsedUserId))
            {
                sqlParameters.Add("@UserIdParameter", parsedUserId, DbType.Int32);
                return _dapper.LoadDataWithParam<Post>(sql, sqlParameters);
            }

            // Return an empty list if userId is not available
            return Enumerable.Empty<Post>();
        }

        public IEnumerable<Post> GetPosts(int postId = 0, int userId = 0, string searchParam = "None")
        {
            throw new NotImplementedException();
        }

        public IActionResult UpsertPost(Post postToUpsert)
        {
            throw new NotImplementedException();
        }
    }
}