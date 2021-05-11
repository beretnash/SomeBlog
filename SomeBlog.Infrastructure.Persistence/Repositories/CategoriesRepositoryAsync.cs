using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Domain.Entities;

namespace SomeBlog.Infrastructure.Persistence.Repositories
{
    public class CategoriesRepositoryAsync : GenericRepositoryAsync<Category>, ICategoriesRepositoryAsync
    {
        public CategoriesRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
