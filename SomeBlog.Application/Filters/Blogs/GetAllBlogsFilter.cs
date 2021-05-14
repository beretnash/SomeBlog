namespace SomeBlog.Application.Filters.Blogs
{
    public class GetAllBlogsFilter : PaginationFilter
    {
        public bool? IsMine { get; set; }
    }
}
