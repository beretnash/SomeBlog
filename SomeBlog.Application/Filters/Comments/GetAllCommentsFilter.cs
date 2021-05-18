namespace SomeBlog.Application.Filters.Comments
{
    public class GetAllCommentsFilter : PaginationFilter
    {
        public string BlogId { get; set; }
    }
}
