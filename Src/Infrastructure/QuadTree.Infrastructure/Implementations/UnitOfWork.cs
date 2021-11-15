using System;
using System.Threading.Tasks;
using QuadTree.Domain.InfrastructureInterfaces;

namespace QuadTree.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuadTreeDbContext _dbContext;

        public UnitOfWork(QuadTreeDbContext dbContext,
            ILocationRepository locationRepository,
            IQuadTreeRepository quadTreeRepository,
            IBoundaryRepository boundaryRepository)
        {
            _dbContext = dbContext;
            LocationRepository = locationRepository;
            QuadTreeRepository = quadTreeRepository;
            BoundaryRepository = boundaryRepository;
            
        }

        public ILocationRepository LocationRepository { get; }
        public IQuadTreeRepository QuadTreeRepository { get; }
        public IBoundaryRepository BoundaryRepository { get; }

        public async Task<int> CommitChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
