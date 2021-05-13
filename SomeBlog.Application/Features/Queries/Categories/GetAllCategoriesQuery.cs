using AutoMapper;
using MediatR;
using SomeBlog.Application.DataTransferObjects.Categories;
using SomeBlog.Application.Filters.Categories;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Queries.Categories
{
    public class GetAllCategoriesQuery : IRequest<PagedResponse<IEnumerable<CategoryResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, PagedResponse<IEnumerable<CategoryResponse>>>
    {
        private readonly ICategoriesRepositoryAsync _categoriesRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(ICategoriesRepositoryAsync categoriesRepositoryAsync, IMapper mapper)
        {
            _categoriesRepositoryAsync = categoriesRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllCategoriesFilter>(request);
            var categories = await _categoriesRepositoryAsync.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var categoryResponse = _mapper.Map<IEnumerable<CategoryResponse>>(categories);
            return new PagedResponse<IEnumerable<CategoryResponse>>(categoryResponse, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
