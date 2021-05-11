using System;

namespace SomeBlog.Application.DataTransferObjects.Comment
{
    public class CommentResponse
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime Modified { get; set; }
        public string AuthorId { get; set; }
    }
}
