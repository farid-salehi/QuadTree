using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuadTree.Domain.InfrastructureInterfaces;

namespace QuadTree.Infrastructure.Implementations
{
    public class QuadTreeRepository: Repository<Domain.Models.QuadTree>, IQuadTreeRepository
    {
        public QuadTreeRepository(QuadTreeDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Domain.Models.QuadTree> GetByBoundary(double topLeftLatitude,
            double topLeftLongitude,
            double topRightLatitude, 
            double topRightLongitude,
            double downLeftLatitude,
            double downLeftLongitude,
            double downrightLatitude,
            double downRightLongitude)
        {
            return await DbContext.QuadTrees
                .FirstOrDefaultAsync(
                x=> 
                    x.Boundary.TopLeft.Latitude.Equals(topLeftLatitude)
                && x.Boundary.TopLeft.Longitude.Equals(topLeftLongitude)
                && x.Boundary.TopRight.Latitude.Equals(topRightLatitude)
                && x.Boundary.TopRight.Longitude.Equals(topRightLongitude)
                && x.Boundary.DownLeft.Latitude.Equals(downLeftLatitude)
                && x.Boundary.DownLeft.Longitude.Equals(downLeftLongitude)
                && x.Boundary.DownRight.Latitude.Equals(downrightLatitude)
                && x.Boundary.DownRight.Longitude.Equals(downRightLongitude)
                );
        }

        public async Task<Domain.Models.QuadTree> GetById(int quadTreeId)
        {
            return await DbContext.QuadTrees
                .Include(x => x.Boundary)
                .Include(x => x.NorthWest)
                .Include(x => x.NorthEast)
                .Include(x => x.SouthWest)
                .Include(x => x.SouthEast)
                .Include(x => x.Boundary.TopLeft)
                .Include(x => x.Boundary.TopRight)
                .Include(x => x.Boundary.DownLeft)
                .Include(x => x.Boundary.DownRight)
                .Include(x => x.Locations)
                .FirstOrDefaultAsync(
                    x => x.QuadTreeId == quadTreeId);


        }
    }
}
