using DotnetAPI.Interfaces;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        public PostService(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }
        public IActionResult DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetMyPosts()
        {
            return this.postRepository.GetMyPosts();
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