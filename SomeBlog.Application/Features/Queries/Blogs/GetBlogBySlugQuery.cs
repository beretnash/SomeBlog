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
    public class GetBlogBySlugQuery : IRequest<Response<BlogResponse>>
    {
        public string Slug { get; set; }
    }

    public class GetBlogBySlugQueryHandler : IRequestHandler<GetBlogBySlugQuery, Response<BlogResponse>>
    {
        private readonly IBlogsRepositoryAsync _blogsRepositoryAsync;
        private readonly IMapper _mapper;

        public GetBlogBySlugQueryHandler(IBlogsRepositoryAsync blogsRepositoryAsync, IMapper mapper)
        {
            _blogsRepositoryAsync = blogsRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<BlogResponse>> Handle(GetBlogBySlugQuery query, CancellationToken cancellationToken)
        {
            var blog = await _blogsRepositoryAsync.GetBySlugAsync(query.Slug);

            if (blog == null)
            {
                throw new Exception($"Blog not found");
            }

            var blogResponse = _mapper.Map<BlogResponse>(blog);
            return new Response<BlogResponse>(blogResponse);
        }
    }
}
