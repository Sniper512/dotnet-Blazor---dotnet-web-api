using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Api.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime PostDate { get; set; }
        public int Stars { get; set; }
        public string Author { get; set; }
        public int LikesCount { get; set; }
        public int DisLikeCount { get; set; }
        public int ShareCount { get; set; }


        // Navigation property
        public virtual ICollection<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
        }
    }
}