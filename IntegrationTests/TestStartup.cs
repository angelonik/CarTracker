using CarTracker;
using DataAccess;
using IntegrationTests.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarTracker_Test;Trusted_Connection=True;MultipleActiveResultSets=true"));
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.EnsureSeeded();
            }

            base.Configure(app, env);
        }
    }
}
