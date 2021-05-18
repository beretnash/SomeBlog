using SomeBlog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SomeBlog.Application.Interfaces.Repositories
{
    public interface ICommentsRepositoryAsync : IGenericRepositoryAsync<Comment>
    {
        Task<IReadOnlyList<Comment>> GetAllByBlogAsync(string blogId);
        Task<IReadOnlyList<Comment>> GetAllByBlogIdPagedReponseAsync(int pageNumber, int pageSize, string blogId);
    }
}
