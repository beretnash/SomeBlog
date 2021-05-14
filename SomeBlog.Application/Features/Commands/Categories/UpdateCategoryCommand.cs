using AutoMapper;
using MediatR;
using SomeBlog.Application.DataTransferObjects.Categories;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Commands.Categories
{
    public class UpdateCategoryCommand : IRequest<Response<CategoryResponse>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Response<CategoryResponse>>
    {
        private readonly ICategoriesRepositoryAsync _categoriesRepositoryAsync;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UpdateCategoryCommandHandler(ICategoriesRepositoryAsync categoriesRepositoryAsync, IMapper mapper)
        {
            _categoriesRepositoryAsync = categoriesRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<CategoryResponse>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoriesRepositoryAsync.GetByIdAsync(command.Id);

            if (category == null)
            {
                throw new Exception($"Category Not Found.");
            }

            category.Title = command.Title;
            await _categoriesRepositoryAsync.UpdateAsync(category);
            var categoryResponse = _mapper.Map<CategoryResponse>(category);
            return new Response<CategoryResponse>(categoryResponse);
        }
    }
}
