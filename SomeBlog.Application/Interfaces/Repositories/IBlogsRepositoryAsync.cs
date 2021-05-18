using SomeBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SomeBlog.Application.Interfaces.Repositories
{
    public interface IBlogsRepositoryAsync : IGenericRepositoryAsync<Blog>
    {
        Task<Blog> GetByIdAndAuthorIdAsync(Guid id, string authorId);
        Task<Blog> GetBySlugAsync(string slug);
        Task<IReadOnlyList<Blog>> GetAllByAuthorAsync(string authorId);
        Task<IReadOnlyList<Blog>> GetAllPublishedPagedReponseAsync(int pageNumber, int pageSize);
        Task<IReadOnlyList<Blog>> GetAllMinePagedReponseAsync(int pageNumber, int pageSize, string authorId);
    }
}
