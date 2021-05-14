using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SomeBlog.Application.DataTransferObjects.Blogs;
using SomeBlog.Application.DataTransferObjects.Categories;
using SomeBlog.Application.Interfaces;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using SomeBlog.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Commands.Blogs
{
    public class CreateBlogCommand : IRequest<Response<BlogResponse>>
    {
        [Required]
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string Content { get; set; }
        public bool? IsPublished { get; set; }
        public string AuthorId { get; set; }
        public Guid[] CategoryIds { get; set; }
    }

    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, Response<BlogResponse>>
    {
        private readonly IBlogsRepositoryAsync _blogsRepositoryAsync;
        private readonly ICategoriesRepositoryAsync _categoriesRepositoryAsync;
        private readonly IImageProvider _imageProvider;
        private readonly IMapper _mapper;

        public CreateBlogCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CreateBlogCommandHandler(IBlogsRepositoryAsync blogsRepositoryAsync, ICategoriesRepositoryAsync categoriesRepositoryAsync, IImageProvider imageProvider, IMapper mapper)
        {
            _blogsRepositoryAsync = blogsRepositoryAsync;
            _categoriesRepositoryAsync = categoriesRepositoryAsync;
            _imageProvider = imageProvider;
            _mapper = mapper;
        }

        public async Task<Response<BlogResponse>> Handle(CreateBlogCommand command, CancellationToken cancellationToken)
        {
            var blog = _mapper.Map<Blog>(command);
            blog.Id = Guid.NewGuid();
            blog.Created = DateTime.UtcNow;
            blog.Modified = DateTime.UtcNow;

            if (blog.IsPublished)
            {
                blog.Published = DateTime.UtcNow;
            }

            blog.ImagePath = await _imageProvider.SaveAsync(command.Image);

            foreach (var categoryId in command.CategoryIds)
            {
                var category = await _categoriesRepositoryAsync
                    .GetByIdAsync(categoryId);

                if (category == null)
                {
                    continue;
                }

                blog.Categories.Add(category);
            }

            await _blogsRepositoryAsync.AddAsync(blog);
            var blogResponse = _mapper.Map<BlogResponse>(blog);
            return new Response<BlogResponse>(blogResponse);
        }
    }
}
