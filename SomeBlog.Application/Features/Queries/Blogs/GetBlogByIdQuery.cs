using AutoMapper;
using MediatR;
using SomeBlog.Application.DataTransferObjects.Blogs;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Queries.Blogs
{
    public class GetBlogByIdQuery : IRequest<Response<BlogResponse>>
    {
        public Guid Id { get; set; }
        public string AuthorId { get; set; }
    }

    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, Response<BlogResponse>>
    {
        private readonly IBlogsRepositoryAsync _blogsRepositoryAsync;
        private readonly IMapper _mapper;

        public GetBlogByIdQueryHandler(IBlogsRepositoryAsync blogsRepositoryAsync, IMapper mapper)
        {
            _blogsRepositoryAsync = blogsRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<BlogResponse>> Handle(GetBlogByIdQuery query, CancellationToken cancellationToken)
        {
            var blog = await _blogsRepositoryAsync.GetByIdAndAuthorIdAsync(query.Id, query.AuthorId);

            if (blog == null)
            {
                throw new Exception($"Blog not found");
            }

            var blogResponse = _mapper.Map<BlogResponse>(blog);
            return new Response<BlogResponse>(blogResponse);
        }
    }
}
