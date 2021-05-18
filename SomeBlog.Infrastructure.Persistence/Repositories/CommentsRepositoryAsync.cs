using Microsoft.EntityFrameworkCore;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SomeBlog.Infrastructure.Persistence.Repositories
{
    public class CommentsRepositoryAsync : GenericRepositoryAsync<Comment>, ICommentsRepositoryAsync
    {
        public CommentsRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IReadOnlyList<Comment>> GetAllByBlogAsync(string blogId)
        {
            return await _dbContext.Comments
                .Where(p => p.BlogId.ToString() == blogId)
                .ToListAsync();
        }
        public async Task<IReadOnlyList<Comment>> GetAllByBlogIdPagedReponseAsync(int pageNumber, int pageSize, string blogId)
        {
            return await _dbContext.Comments
                .Where(p => p.BlogId.ToString() == blogId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
