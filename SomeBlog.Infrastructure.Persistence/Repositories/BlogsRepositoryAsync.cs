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

        public override async Task<Blog> GetByIdAsync(Guid id)
        {
            return await _dbContext.Blogs
                .Include(t => t.Categories)
                .Include(t => t.Comments)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Blog> GetByIdAndAuthorIdAsync(Guid id, string authorId)
        {
            return await _dbContext.Blogs
                .Include(t => t.Categories)
                .Include(t => t.Comments)
                .SingleOrDefaultAsync(p => p.Id == id && p.AuthorId == authorId);
        }

        public async Task<Blog> GetBySlugAsync(string slug)
        {
            return await _dbContext.Blogs
                .Include(t => t.Categories)
                .Include(t => t.Comments)
                .SingleOrDefaultAsync(p => p.Slug == slug);
        }

        public override async Task<IReadOnlyList<Blog>> GetAllAsync()
        {
            return await _dbContext.Blogs
                .Where(p => p.IsPublished)
                .ToListAsync();
        }

        public override async Task<IReadOnlyList<Blog>> GetAllPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.Blogs
                .Where(p => p.IsPublished)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Blog>> GetAllPublishedPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.Blogs
                .Where(p => p.IsPublished)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Blog>> GetAllMinePagedReponseAsync(int pageNumber, int pageSize, string authorId)
        {
            return await _dbContext.Blogs
                .Where(p => p.AuthorId == authorId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Blog>> GetAllByAuthorAsync(string authorId)
        {
            return await _dbContext.Blogs
                .Where(p => p.AuthorId == authorId.ToString())
                .ToListAsync();
        }
    }
}
