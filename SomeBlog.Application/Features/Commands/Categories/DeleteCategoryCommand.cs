using MediatR;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Commands.Categories
{
    public class DeleteCategoryCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<Guid>>
    {
        private readonly ICategoriesRepositoryAsync _categoriesRepositoryAsync;

        public DeleteCategoryCommandHandler(ICategoriesRepositoryAsync categoriesRepositoryAsync)
        {
            _categoriesRepositoryAsync = categoriesRepositoryAsync;
        }

        public async Task<Response<Guid>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoriesRepositoryAsync.GetByIdAsync(command.Id);

            if (category == null)
            {
                throw new Exception($"Category Not Found.");
            }

            await _categoriesRepositoryAsync.DeleteAsync(category);
            return new Response<Guid>(category.Id);
        }
    }
}
