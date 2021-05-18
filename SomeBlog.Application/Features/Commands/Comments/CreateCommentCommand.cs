using AutoMapper;
using MediatR;
using SomeBlog.Application.DataTransferObjects.Comment;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using SomeBlog.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Commands.Comments
{
    public class CreateCommentCommand : IRequest<Response<CommentResponse>>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string AuthorId { get; set; }
        [Required]
        public string BlogId { get; set; }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<CommentResponse>>
    {
        private readonly ICommentsRepositoryAsync _commentsRepositoryAsync;
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<Response<CommentResponse>> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Comment>(command);
            comment.Id = Guid.NewGuid();
            comment.Created = DateTime.UtcNow;
            comment.Modified = DateTime.UtcNow;

            await _commentsRepositoryAsync.AddAsync(comment);
            var CommentResponse = _mapper.Map<CommentResponse>(comment);
            return new Response<CommentResponse>(CommentResponse);
        }
    }
}
