using AutoMapper;
using SomeBlog.Application.Features.Queries.Categories;
using SomeBlog.Application.Filters.Categories;

namespace SomeBlog.Application.MappingProfiles
{
    public class RequestToFilterProfile : Profile
    {
        public RequestToFilterProfile()
        {
            CreateMap<GetAllCategoriesQuery, GetAllCategoriesFilter>();
        }
    }
}
