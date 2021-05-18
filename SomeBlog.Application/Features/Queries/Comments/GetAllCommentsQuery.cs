using AutoMapper;
using MediatR;
using SomeBlog.Application.DataTransferObjects.Comment;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using SomeBlog.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Queries.Comments
{
    public class GetAllCommentsQuery : IRequest<PagedResponse<IEnumerable<CommentResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string BlogId { get; set; }
    }

    public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, PagedResponse<IEnumerable<CommentResponse>>>
    {
        private readonly ICommentsRepositoryAsync _commentsRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllCommentsQueryHandler(ICommentsRepositoryAsync commentsRepositoryAsync, IMapper mapper)
        {
            _commentsRepositoryAsync = commentsRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CommentResponse>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Comment> comments;

            if (string.IsNullOrEmpty(request.BlogId))
            {
                comments = await _commentsRepositoryAsync.GetAllByBlogIdPagedReponseAsync(request.PageNumber, request.PageSize, request.BlogId);
            }
            else
            {
                comments = await _commentsRepositoryAsync.GetAllPagedReponseAsync(request.PageNumber, request.PageSize);
            }

            var commentResponse = _mapper.Map<IEnumerable<CommentResponse>>(comments);
            return new PagedResponse<IEnumerable<CommentResponse>>(commentResponse, request.PageNumber, request.PageSize);
        }
    }
}
