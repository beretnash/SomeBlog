using Microsoft.EntityFrameworkCore;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SomeBlog.Infrastructure.Persistence.Repositories
{
    public class BlogsRepositoryAsync : GenericRepositoryAsync<Blog>, IBlogsRepositoryAsync
    {
        public BlogsRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext) { }

        public override async Task<IReadOnlyList<Blog>> GetAllAsync()
        {
            return await _dbContext.Blogs
                .Where(p => p.IsPublished)
                .ToListAsync();
        }

        public override async Task<IReadOnlyList<Blog>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.Blogs
                .Where(p => p.IsPublished)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Blog>> GetAllByAuthorAsync(Guid authorId)
        {
            return await _dbContext.Blogs
                .Where(p => p.AuthorId == authorId.ToString())
                .ToListAsync();
        }

        public async Task<Blog> GetBySlugAsync(string slug)
        {
            return await _dbContext.Blogs
                .SingleOrDefaultAsync(p => p.Slug == slug);
        }
    }
}
