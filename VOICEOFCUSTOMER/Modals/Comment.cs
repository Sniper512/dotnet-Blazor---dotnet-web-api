using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Web_Api.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        public int Likes { get; set; }

        [Required]
        public int PostId { get; set; }

        // Navigation property
        [JsonIgnore] // Add this to prevent serialization issues
        public virtual Post? Post { get; set; }
    }
}

