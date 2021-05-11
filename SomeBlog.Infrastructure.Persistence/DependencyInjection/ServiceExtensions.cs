using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SomeBlog.Application.Interfaces.Repositories;
using SomeBlog.Infrastructure.Persistence.Repositories;

namespace SomeBlog.Infrastructure.Persistence.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IBlogsRepositoryAsync, BlogsRepositoryAsync>();
            services.AddScoped<ICategoriesRepositoryAsync, CategoriesRepositoryAsync>();
            services.AddScoped<ICommentsRepositoryAsync, CommentsRepositoryAsync>();
        }
    }
}
