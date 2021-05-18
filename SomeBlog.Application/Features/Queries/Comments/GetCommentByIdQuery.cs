using AutoMapper;
using MediatR;
using SomeBlog.Application.DataTransferObjects.Comment;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Queries.Comments
{
    public class GetCommentByIdQuery : IRequest<Response<CommentResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, Response<CommentResponse>>
    {
        private readonly ICommentsRepositoryAsync _commentsRepositoryAsync;
        private readonly IMapper _mapper;

        public GetCommentByIdQueryHandler(ICommentsRepositoryAsync commentsRepositoryAsync)
        {
            _commentsRepositoryAsync = commentsRepositoryAsync;
        }

        public async Task<Response<CommentResponse>> Handle(GetCommentByIdQuery query, CancellationToken cancellationToken)
        {
            var comment = await _commentsRepositoryAsync.GetByIdAsync(query.Id);

            if (comment == null)
            {
                throw new Exception($"Comment not found");
            }

            var commentResponse = _mapper.Map<CommentResponse>(comment);
            return new Response<CommentResponse>(commentResponse);
        }
    }
}
