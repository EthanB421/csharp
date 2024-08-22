using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DotnetAPI.Models
{
    public partial class PostToEditDto
    {
        public string PostId{get; set;}
        public string PostTitle{get; set;}
        public string PostContent{get; set;}

        public PostToEditDto()
        {
            if (PostTitle == null)
            {
                PostTitle = "";
            }
            if (PostContent == null)
            {
                PostContent = "";
            }
        }
    }
}