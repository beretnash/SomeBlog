using AutoMapper;
using SomeBlog.Application.Features.Commands.Blogs;
using SomeBlog.Application.Features.Commands.Categories;
using SomeBlog.Domain.Entities;

namespace SomeBlog.Application.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<CreateBlogCommand, Blog>();
        }
    }
}
