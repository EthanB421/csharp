using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DotnetAPI.Models
{
    public class Post
    {
        public int PostId{get; set;}
        public string UserId{get; set;} ="";
        public string PostTitle{get; set;} ="";
        public string PostContent{get; set;} ="";
        public DateTime PostCreated{get; set;}
        public DateTime PostUpdated{get; set;}

        public Post()
        {

        }
    }
}