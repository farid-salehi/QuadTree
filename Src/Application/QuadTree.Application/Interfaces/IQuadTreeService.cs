using System.Collections.Generic;
using QuadTree.Domain.DataTransferObjects;
using QuadTree.Domain.Models;

namespace QuadTree.Application.Interfaces
{
    public interface IQuadTreeService
    {
        bool Insert(InputLocationDto location);

        IEnumerable<LocationDto> GetLocations(Location location, int maxDistance, int maxResults);
    }
}
