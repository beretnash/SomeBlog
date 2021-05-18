using AutoMapper;
using MediatR;
using SomeBlog.Application.DataTransferObjects.Blogs;
using SomeBlog.Application.Filters.Blogs;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Queries.Blogs
{
    public class GetAllOwnBlogsQuery : IRequest<PagedResponse<IEnumerable<GetAllPublishedBlogsResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string AuthorId { get; set; }
    }

    public class GetAllOwnBlogsQueryHandler : IRequestHandler<GetAllOwnBlogsQuery, PagedResponse<IEnumerable<GetAllPublishedBlogsResponse>>>
    {
        private readonly IBlogsRepositoryAsync _blogsRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllOwnBlogsQueryHandler(IBlogsRepositoryAsync blogsRepositoryAsync, IMapper mapper)
        {
            _blogsRepositoryAsync = blogsRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllPublishedBlogsResponse>>> Handle(GetAllOwnBlogsQuery request, CancellationToken cancellationToken)
        {
            var blogs = await _blogsRepositoryAsync.GetAllMinePagedReponseAsync(request.PageNumber, request.PageSize, request.AuthorId);
            var blogResponse = _mapper.Map<IEnumerable<GetAllPublishedBlogsResponse>>(blogs);
            return new PagedResponse<IEnumerable<GetAllPublishedBlogsResponse>>(blogResponse, request.PageNumber, request.PageSize);
        }
    }
}
