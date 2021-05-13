using AutoMapper;
using MediatR;
using SomeBlog.Application.DataTransferObjects.Categories;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Queries.Categories
{
    public class GetCategoryByIdQuery : IRequest<Response<CategoryResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Response<CategoryResponse>>
    {
        private readonly ICategoriesRepositoryAsync _categoriesRepositoryAsync;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICategoriesRepositoryAsync categoriesRepositoryAsync, IMapper mapper)
        {
            _categoriesRepositoryAsync = categoriesRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<CategoryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var story = await _categoriesRepositoryAsync.GetByIdAsync(query.Id);

            if (story == null)
            {
                throw new Exception($"Category not found");
            }

            var storyViewModel = _mapper.Map<CategoryResponse>(story);
            return new Response<CategoryResponse>(storyViewModel);
        }
    }
}
