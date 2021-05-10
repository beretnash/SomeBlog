using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SomeBlog.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}
