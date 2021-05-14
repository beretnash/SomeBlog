using AutoMapper;
using SomeBlog.Application.DataTransferObjects.Blogs;
using SomeBlog.Application.DataTransferObjects.Categories;
using SomeBlog.Application.DataTransferObjects.Comment;
using SomeBlog.Domain.Entities;

namespace SomeBlog.Application.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Category, CategoryResponse>();
            CreateMap<Comment, CommentResponse>();
            CreateMap<Blog, BlogResponse>();
            CreateMap<Blog, GetAllPublishedBlogsResponse>();
        }
    }
}
