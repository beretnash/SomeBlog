using AutoMapper;
using MediatR;
using SomeBlog.Application.DataTransferObjects.Categories;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using SomeBlog.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Commands.Categories
{
    public class CreateCategoryCommand : IRequest<Response<CategoryResponse>>
    {
        public string Title { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<CategoryResponse>>
    {
        private readonly ICategoriesRepositoryAsync _categoriesRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CreateCategoryCommandHandler(ICategoriesRepositoryAsync categoriesRepositoryAsync, IMapper mapper)
        {
            _categoriesRepositoryAsync = categoriesRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<CategoryResponse>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(command);
            category.Id = Guid.NewGuid();
            category.Created = DateTime.UtcNow;
            await _categoriesRepositoryAsync.AddAsync(category);
            var categoryResponse = _mapper.Map<CategoryResponse>(category);
            return new Response<CategoryResponse>(categoryResponse);
        }
    }
}
