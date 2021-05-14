using SomeBlog.Application.DataTransferObjects.Categories;
using SomeBlog.Application.DataTransferObjects.Comment;
using System.Collections.Generic;

namespace SomeBlog.Application.DataTransferObjects.Blogs
{
    public class BlogResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ImagePath { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public ICollection<CommentResponse> Comments { get; set; }
        public ICollection<CategoryResponse> Categories { get; set; }
    }
}
