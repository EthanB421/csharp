using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Interfaces
{
    public interface IPostService{
        public IEnumerable<Post> GetPosts(int postId = 0, int userId = 0, string searchParam = "None"); 
        public IEnumerable<Post> GetMyPosts();
    
        public IActionResult UpsertPost(Post postToUpsert);
        public IActionResult DeletePost(int postId);

    }
}