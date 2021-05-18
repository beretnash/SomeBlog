using AutoMapper;
using MediatR;
using SomeBlog.Application.DataTransferObjects.Comment;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Commands.Comments
{
    public class UpdateCommentCommand : IRequest<Response<CommentResponse>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; }
    }

    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response<CommentResponse>>
    {
        private readonly ICommentsRepositoryAsync _commentsRepositoryAsync;
        private readonly IMapper _mapper;

        public UpdateCommentCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UpdateCommentCommandHandler(ICommentsRepositoryAsync commentsRepositoryAsync, IMapper mapper)
        {
            _commentsRepositoryAsync = commentsRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<CommentResponse>> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            var comment = await _commentsRepositoryAsync.GetByIdAsync(command.Id);

            if (comment == null)
            {
                throw new Exception($"Comment Not Found.");
            }

            comment.Content = command.Content;
            comment.Modified = DateTime.UtcNow;

            await _commentsRepositoryAsync.UpdateAsync(comment);
            var CommentResponse = _mapper.Map<CommentResponse>(comment);
            return new Response<CommentResponse>(CommentResponse);
        }
    }
}
