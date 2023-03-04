using System.ComponentModel.DataAnnotations;

namespace Psinder.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Size { get; set; }
        public string Breed { get; set; }
        public string Difficulty { get; set; }
        public string Location { get; set; }

        public string Description { get; set; }

        public string? ImagePath { get; set; }

        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
    }
}
