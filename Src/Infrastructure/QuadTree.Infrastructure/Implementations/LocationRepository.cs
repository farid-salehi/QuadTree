using QuadTree.Domain.InfrastructureInterfaces;
using QuadTree.Domain.Models;


namespace QuadTree.Infrastructure.Implementations
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(QuadTreeDbContext dbContext) : base(dbContext)
        {
        }
    }
}
