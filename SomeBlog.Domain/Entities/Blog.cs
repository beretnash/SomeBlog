using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SomeBlog.Domain.Entities
{
    public class Blog : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        public string ImagePath { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public DateTime Modified { get; set; }
        public bool IsPublished { get; set; }
        public string AuthorId { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
