using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomeBlog.Domain.Entities
{
    public class Comment : BaseEntity
    {
        [Required]
        public string Content { get; set; }
        public DateTime Modified { get; set; }
        public Guid AuthorId { get; set; }


        [ForeignKey(nameof(BlogId))]
        public Blog Blog { get; set; }
        public Guid BlogId { get; set; }
    }
}
