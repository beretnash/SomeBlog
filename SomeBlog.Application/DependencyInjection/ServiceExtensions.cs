using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SomeBlog.Application.Interfaces;
using SomeBlog.Application.Providers;
using System.Reflection;

namespace SomeBlog.Application.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IImageProvider, ImageProvider>();
        }
    }
}
