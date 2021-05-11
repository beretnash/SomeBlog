using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SomeBlog.Infrastructure.Identity.DependencyInjection;
using SomeBlog.Infrastructure.Persistence.DependencyInjection;
using SomeBlog.WebApi.Extensions;

namespace SomeBlog.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistenceInfrastructure(Configuration);
            services.AddIdentityContext(Configuration);
            services.AddJwtAuthorization(Configuration);
            services.AddSwagger();
            services.AddApiVersioningExtension();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwaggerExtension(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
