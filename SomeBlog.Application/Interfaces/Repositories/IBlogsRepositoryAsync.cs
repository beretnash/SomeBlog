using SomeBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SomeBlog.Application.Interfaces.Repositories
{
    public interface IBlogsRepositoryAsync : IGenericRepositoryAsync<Blog>
    {
        Task<Blog> GetBySlugAsync(string slug);
        Task<IReadOnlyList<Blog>> GetAllByAuthorAsync(Guid authorId);
    }
}
