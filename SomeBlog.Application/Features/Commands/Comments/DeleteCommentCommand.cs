using MediatR;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Commands.Comments
{
    public class DeleteCommentCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Response<Guid>>
    {
        private readonly ICommentsRepositoryAsync _commentsRepositoryAsync;

        public DeleteCommentCommandHandler(ICommentsRepositoryAsync commentsRepositoryAsync)
        {
            _commentsRepositoryAsync = commentsRepositoryAsync;
        }

        public async Task<Response<Guid>> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
        {
            var Comment = await _commentsRepositoryAsync.GetByIdAsync(command.Id);

            if (Comment == null)
            {
                throw new Exception($"Comment Not Found.");
            }

            await _commentsRepositoryAsync.DeleteAsync(Comment);
            return new Response<Guid>(Comment.Id);
        }
    }
}
