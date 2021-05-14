using AutoMapper;
using MediatR;
using SomeBlog.Application.DataTransferObjects.Blogs;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Queries.Blogs
{
    public class GetAllPublishedBlogsQuery : IRequest<PagedResponse<IEnumerable<GetAllPublishedBlogsResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllPublishedBlogsQueryHandler : IRequestHandler<GetAllPublishedBlogsQuery, PagedResponse<IEnumerable<GetAllPublishedBlogsResponse>>>
    {
        private readonly IBlogsRepositoryAsync _blogsRepositoryAsync;
        private readonly IMapper _mapper;

        public GetAllPublishedBlogsQueryHandler(IBlogsRepositoryAsync blogsRepositoryAsync, IMapper mapper)
        {
            _blogsRepositoryAsync = blogsRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllPublishedBlogsResponse>>> Handle(GetAllPublishedBlogsQuery request, CancellationToken cancellationToken)
        {
            var blogs = await _blogsRepositoryAsync.GetAllPublishedPagedReponseAsync(request.PageNumber, request.PageSize);
            var blogResponse = _mapper.Map<IEnumerable<GetAllPublishedBlogsResponse>>(blogs);
            return new PagedResponse<IEnumerable<GetAllPublishedBlogsResponse>>(blogResponse, request.PageNumber, request.PageSize);
        }
    }
}
