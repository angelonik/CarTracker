using DataAccess;
using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Services;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;
using System.Net.Mime;

namespace CarTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);
            services
                .AddTransient<IUserService, UserService>()
                .AddTransient<IManufacturerService, ManufacturerService>()
                // added for blazor
                .AddResponseCompression(options =>
                {
                    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                    {
                        MediaTypeNames.Application.Octet,
                        WasmMediaTypeNames.Application.Wasm,
                    });
                })
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "CarTracker API", Version = "v1" });
                })
                .AddMvc().AddJsonOptions(options =>
                {
                    // needed for blazor to work
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
        }

        public virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options => options
                .UseLoggerFactory(ApplicationContext.ConsoleLoggerFactory)
                .EnableSensitiveDataLogging(true)
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarTracker API V1");
                })
                .UseMvc()
                .UseBlazor<Client.Program>();
        }
    }
}
