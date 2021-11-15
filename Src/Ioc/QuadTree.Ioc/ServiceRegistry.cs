using Microsoft.Extensions.DependencyInjection;
using QuadTree.Application.Implementations;
using QuadTree.Application.Interfaces;
using QuadTree.Domain.InfrastructureInterfaces;
using QuadTree.Infrastructure.Implementations;

namespace QuadTree.Ioc
{
    public static class ServiceRegistry
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IQuadTreeRepository, QuadTreeRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IBoundaryRepository, BoundaryRepository>();
            services.AddScoped<IQuadTreeService, QuadTreeService>();
         
            return services;
        }
    }
}
