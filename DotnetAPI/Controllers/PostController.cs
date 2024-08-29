using System.Data;
using Dapper;
using DotnetAPI.Interfaces;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        private readonly IPostService postService;
        public PostController(IConfiguration config, IPostService postService)
        {
            _dapper = new DataContextDapper(config);
            this.postService = postService;
        }

        [HttpGet("Posts/{postId}/{userId}/{searchParam}")]
        public IEnumerable<Post> GetPosts(int postId = 0, int userId = 0, string searchParam = "None")
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Get";
            string parameters = "";
            DynamicParameters sqlParameters = new DynamicParameters();
            if(postId != 0)
            {
                parameters += ", @PostId=@PostIdParameter";
                sqlParameters.Add("@PostIdParameter", postId, DbType.Int32);
            }
            if(userId != 0)
            {
                parameters += ", @userId=@UserIdParameter";
                sqlParameters.Add("@UserIdParameter", userId, DbType.Int32);

            }
            if(searchParam != "None")
            {
                parameters += ", @SearchValue=@SearchValueParameter";
                sqlParameters.Add("@SearchValueParameter", searchParam, DbType.String);
            }

            if(parameters.Length > 0)
            {
            sql += parameters.Substring(1);
            }
            return _dapper.LoadDataWithParam<Post>(sql, sqlParameters);
        }

        [HttpGet("MyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {
            return postService.GetMyPosts();
        }



        [HttpPut("UpsertPost")]
        public IActionResult UpsertPost(Post postToUpsert)
        {
            string sql = @"EXEC TutorialAppSchema.spPosts_Upsert
            @UserId = @UserIdParameter,
            @PostTitle = @PostTitleParameter,
            @PostContent = @PostContentParameter";
            DynamicParameters sqlParameters = new DynamicParameters();
            sqlParameters.Add("@UserIdParameter", this.User.FindFirst("userId")?.Value, DbType.Int32);
            sqlParameters.Add("@PostTitleParameter", postToUpsert.PostTitle, DbType.String);
            sqlParameters.Add("@PostContentParameter", postToUpsert.PostContent, DbType.String);


            if (postToUpsert.PostId > 0) {
                sql += ", @PostId = @PostIdParameter";
                sqlParameters.Add("@PostIdParameter", postToUpsert.PostId, DbType.Int32);
            }

            if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters))
            {
                return Ok();
            }throw new Exception("Failed to upsert post");
        }

        // [HttpPut("Post")]
        // public IActionResult EditPost(PostToEditDto postToEdit)
        // {
        //     string sql = @"UPDATE TutorialAppSchema.Posts SET 
        //                 PostContent = '" + postToEdit.PostContent + "', PostTitle = '" + postToEdit.PostTitle + 
        //                 @"', PostUpdated = GETDATE()
        //                 WHERE PostId = "+postToEdit.PostId.ToString()+
        //                 "AND UserId = " + this.User.FindFirst("userId")?.Value;

        //     if (_dapper.ExecuteSql(sql))
        //     {
        //         return Ok();
        //     }throw new Exception("Failed to edit post");
        // }

        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            string sql = "EXEC TutorialAppSchema.spPost_Delete @PostId = @PostIdParameter, @UserId = @UserIdParameter";
            DynamicParameters sqlParameters = new DynamicParameters();
            sqlParameters.Add("@UserIdParameter", this.User.FindFirst("userId")?.Value, DbType.Int32);
            sqlParameters.Add("@PostIdParameter", postId, DbType.Int32);


            if (_dapper.ExecuteSqlWithParameters(sql, sqlParameters))
            {
                return Ok();
            }throw new Exception("Failed to delete post");
        }

        
    }
}