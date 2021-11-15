using System;
using System.Threading.Tasks;

namespace QuadTree.Domain.InfrastructureInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILocationRepository LocationRepository { get; }
        IQuadTreeRepository QuadTreeRepository { get; }
        IBoundaryRepository BoundaryRepository { get; }
        Task<int> CommitChangesAsync();

    }
}
