using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using QuadTree.Domain.DataTransferObjects;
using QuadTree.Infrastructure;
using QuadTree.Ioc;
using QuadTree.WebApi.Middleware;


namespace QuadTree.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath);
            if (environment.EnvironmentName == "Development")
            {
                builder.AddJsonFile($"appsettings.Development.json", optional: false);
            }
            else
            {
                builder.AddJsonFile("appsettings.Production.json", optional: false, reloadOnChange: true);
            }
            builder.Build();
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QuadTreeDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("QuadTreeConnectionString")));
            services.AddControllers();

            services.Configure<Settings>(options =>
                Configuration.GetSection("Settings").Bind(options));

            services.RegisterServices();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Quad-Tree",
                    Description = "Quad-Tree Web Service"
                });
            });
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, QuadTreeDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting(); 
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=FileManagerView}/{action=Index}/{id?}");
            });


            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            db.Database.EnsureCreated();
        }
    }
}
