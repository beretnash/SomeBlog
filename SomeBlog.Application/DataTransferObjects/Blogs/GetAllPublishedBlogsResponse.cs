using SomeBlog.Application.DataTransferObjects.Categories;
using System.Collections.Generic;

namespace SomeBlog.Application.DataTransferObjects.Blogs
{
    public class GetAllPublishedBlogsResponse
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ImagePath { get; set; }
        public int CommnetsCount { get; set; }
        public ICollection<CategoryResponse> Categories { get; set; }
    }
}
