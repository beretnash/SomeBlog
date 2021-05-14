using MediatR;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Commands.Blogs
{
    public class DeleteBlogCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, Response<Guid>>
    {
        private readonly IBlogsRepositoryAsync _blogsRepositoryAsync;

        public DeleteBlogCommandHandler(IBlogsRepositoryAsync blogsRepositoryAsync)
        {
            _blogsRepositoryAsync = blogsRepositoryAsync;
        }

        public async Task<Response<Guid>> Handle(DeleteBlogCommand command, CancellationToken cancellationToken)
        {
            var blog = await _blogsRepositoryAsync.GetByIdAsync(command.Id);

            if (blog == null)
            {
                throw new Exception($"Blog Not Found.");
            }

            await _blogsRepositoryAsync.DeleteAsync(blog);
            return new Response<Guid>(blog.Id);
        }
    }
}
