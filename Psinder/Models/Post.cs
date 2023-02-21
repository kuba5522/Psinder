﻿using System.ComponentModel.DataAnnotations;

namespace Psinder.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } 

        public string Description { get; set; } 
    }
}
