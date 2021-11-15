using System.Threading.Tasks;

namespace QuadTree.Domain.InfrastructureInterfaces
{
    public interface IQuadTreeRepository : IRepository<Domain.Models.QuadTree>
    {
        Task<Models.QuadTree> GetByBoundary(double topLeftLatitude,
            double topLeftLongitude,
            double topRightLatitude,
            double topRightLongitude,
            double downLeftLatitude,
            double downLeftLongitude,
            double downrightLatitude,
            double downRightLongitude);
        Task<Models.QuadTree> GetById(int quadTreeId);
    }
}
