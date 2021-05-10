using System;
using System.ComponentModel.DataAnnotations;

namespace SomeBlog.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
    }
}
