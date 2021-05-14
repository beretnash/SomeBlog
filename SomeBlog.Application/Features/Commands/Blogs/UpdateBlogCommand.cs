using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using SomeBlog.Application.DataTransferObjects.Blogs;
using SomeBlog.Application.Interfaces;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Application.Wrappers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SomeBlog.Application.Features.Commands.Blogs
{
    public class UpdateBlogCommand : IRequest<Response<BlogResponse>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public DateTime Modified { get; set; }
        public bool IsPublished { get; set; }
        public string AuthorId { get; set; }
        public Guid[] CategoryIds { get; set; }
    }

    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, Response<BlogResponse>>
    {
        private readonly IBlogsRepositoryAsync _blogsRepositoryAsync;
        private readonly ICategoriesRepositoryAsync _categoriesRepositoryAsync;
        private readonly IImageProvider _imageProvider;
        private readonly IMapper _mapper;

        public UpdateBlogCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UpdateBlogCommandHandler(IBlogsRepositoryAsync blogsRepositoryAsync, ICategoriesRepositoryAsync categoriesRepositoryAsync, IImageProvider imageProvider, IMapper mapper)
        {
            _blogsRepositoryAsync = blogsRepositoryAsync;
            _categoriesRepositoryAsync = categoriesRepositoryAsync;
            _imageProvider = imageProvider;
            _mapper = mapper;
        }

        public async Task<Response<BlogResponse>> Handle(UpdateBlogCommand command, CancellationToken cancellationToken)
        {
            var blog = await _blogsRepositoryAsync.GetByIdAndAuthorIdAsync(command.Id, command.AuthorId);

            if (blog == null)
            {
                throw new Exception($"Blog Not Found.");
            }

            blog.Title = command.Title;
            blog.Content = command.Content;
            blog.Modified = DateTime.UtcNow;

            var updatedImagePath = await _imageProvider.SaveAsync(command.Image);

            if (string.IsNullOrEmpty(updatedImagePath) == false)
            {
                blog.ImagePath = updatedImagePath;
            }

            if (blog.IsPublished)
            {
                blog.Published = DateTime.UtcNow;
            }

            foreach (var categoryId in command.CategoryIds)
            {
                var category = await _categoriesRepositoryAsync
                    .GetByIdAsync(categoryId);

                if (category == null || blog.Categories.FirstOrDefault(t => t.Id == categoryId) != null)
                {
                    continue;
                }

                blog.Categories.Add(category);
            }

            await _blogsRepositoryAsync.UpdateAsync(blog);
            var BlogResponse = _mapper.Map<BlogResponse>(blog);
            return new Response<BlogResponse>(BlogResponse);
        }
    }
}
