using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SomeBlog.Application.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
