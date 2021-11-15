using QuadTree.Domain.InfrastructureInterfaces;
using QuadTree.Domain.Models;

namespace QuadTree.Infrastructure.Implementations
{
    public class BoundaryRepository : Repository<Boundary>, IBoundaryRepository
    {
        public BoundaryRepository(QuadTreeDbContext dbContext) : base(dbContext)
        {
        }
    }
}
