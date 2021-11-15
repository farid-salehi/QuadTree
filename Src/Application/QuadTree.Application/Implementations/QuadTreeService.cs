using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using QuadTree.Application.Extensions;
using QuadTree.Application.Interfaces;
using QuadTree.Domain.DataTransferObjects;
using QuadTree.Domain.InfrastructureInterfaces;
using QuadTree.Domain.Models;


namespace QuadTree.Application.Implementations
{
    public class QuadTreeService : IQuadTreeService
    {
        private readonly Settings _settings;
        private readonly IUnitOfWork _unitOfWork;
        public QuadTreeService(IOptions<Settings> settings,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _settings = settings.Value;
        }


        private Domain.Models.QuadTree GetMainQuadTree()
        {
            Domain.Models.QuadTree mainQuadTree =
            _unitOfWork.QuadTreeRepository.GetByBoundary(
                _settings.TopLeftLatitude,
                _settings.TopLeftLongitude,
                _settings.TopRightLatitude,
                _settings.TopRightLongitude,
                _settings.DownLeftLatitude,
                _settings.DownLeftLongitude,
                _settings.DownRightLatitude,
                _settings.DownRightLongitude).Result;

            if (mainQuadTree == null)
            {
                mainQuadTree = new Domain.Models.QuadTree()
                {
                    Capacity = _settings.DefaultQuadTreeCapacity,
                    Boundary = new Boundary()
                    {
                        TopLeft = new Location(_settings.TopLeftLatitude, _settings.TopLeftLongitude),
                        TopRight = new Location(_settings.TopRightLatitude, _settings.TopRightLongitude),
                        DownLeft = new Location(_settings.DownLeftLatitude, _settings.DownLeftLongitude),
                        DownRight = new Location(_settings.DownRightLatitude, _settings.DownRightLongitude),
                    }
                };
                _unitOfWork.QuadTreeRepository.Add(mainQuadTree);
                _unitOfWork.CommitChangesAsync().Wait();
            }
            return mainQuadTree;
        }

 
        public bool Insert(InputLocationDto location)
        {
            if (location == null)
            {
                return false;
            }
            var  mainTree = GetMainQuadTree();
           var result = RecursiveInsert(mainTree.QuadTreeId, new Location(location.Latitude, location.Longitude));
           _unitOfWork.CommitChangesAsync().Wait();
           return result;
        }


        private bool RecursiveInsert(int quadTreeId, Location location)
        {
            var quadTree = _unitOfWork.QuadTreeRepository.GetById(quadTreeId).Result;
            if (!quadTree.Boundary.AtRange(location))
            {
                return false;
            }

            if (quadTree.Locations.Count < quadTree.Capacity)
            {
                quadTree.Locations.Add(location);
                return true;
            }

            if (!quadTree.Divided)
            {
                Divide(quadTree);
                 _unitOfWork.CommitChangesAsync().Wait();
            }


            if (RecursiveInsert(quadTree.NorthWest.QuadTreeId, location))
            {
                return true;
            }

            if (RecursiveInsert(quadTree.NorthEast.QuadTreeId, location))
            {
                return true;
            }

            return RecursiveInsert(quadTree.SouthWest.QuadTreeId, location) || RecursiveInsert(quadTree.SouthEast.QuadTreeId, location);
        }

        private void Divide(Domain.Models.QuadTree quadTree)
        {
            var topLeft = quadTree.Boundary.TopLeft;
            var topRight = quadTree.Boundary.TopRight;
            var topCenter = topRight.FindMiddle(topLeft);

            var downLeft = quadTree.Boundary.DownLeft;
            var downRight = quadTree.Boundary.DownRight;
            var downCenter = downRight.FindMiddle(downLeft);


            var middleLeft = topLeft.FindMiddle(downLeft);
            var middleRight = topRight.FindMiddle(downRight);
            var middleCenter = topCenter.FindMiddle(downCenter);


            quadTree.NorthWest = new Domain.Models.QuadTree()
            {
                Capacity = _settings.DefaultQuadTreeCapacity,
                Boundary = new Boundary()
                {
                    TopLeft = topLeft,
                    TopRight = topCenter,
                    DownLeft = middleLeft,
                    DownRight = middleCenter,
                }
            };

            quadTree.NorthEast = new Domain.Models.QuadTree()
            {
                Capacity = _settings.DefaultQuadTreeCapacity,
                Boundary = new Boundary()
                {
                    TopLeft = topCenter,
                    TopRight = topRight,
                    DownLeft = middleCenter,
                    DownRight = middleRight,
                }
            };

            quadTree.SouthWest = new Domain.Models.QuadTree()
            {
                Capacity = _settings.DefaultQuadTreeCapacity,
                Boundary = new Boundary()
                {
                    TopLeft = middleLeft,
                    TopRight = middleCenter,
                    DownLeft = downLeft,
                    DownRight = downCenter,
                }
            };

            quadTree.SouthEast = new Domain.Models.QuadTree()
            {
                Capacity = _settings.DefaultQuadTreeCapacity,
                Boundary = new Boundary()
                {
                    TopLeft = middleCenter,
                    TopRight = middleRight,
                    DownLeft = downCenter,
                    DownRight = downRight,
                }
            };

            quadTree.Divided = true;
          
        }

        private IEnumerable<Location> GetAllLocationByBoundary
            (int quadTreeId, Boundary range, List<Location> foundedLocations)
        {
            foundedLocations ??= new List<Location>();
            var quadTree = _unitOfWork.QuadTreeRepository.GetById(quadTreeId).Result;
            if (!quadTree.Boundary.Intersects(range))
            {
                return foundedLocations;
            }

            foreach (var location in quadTree.Locations)
            {
                if (range.AtRange(location))
                {
                    foundedLocations.Add(location);
                }
            }

            if (quadTree.Divided)
            {
                GetAllLocationByBoundary(quadTree.NorthWest.QuadTreeId,range, foundedLocations);
                GetAllLocationByBoundary(quadTree.NorthEast.QuadTreeId, range, foundedLocations);
                GetAllLocationByBoundary(quadTree.SouthWest.QuadTreeId, range, foundedLocations);
                GetAllLocationByBoundary(quadTree.SouthEast.QuadTreeId, range, foundedLocations);
            }

            return foundedLocations;
        }

        public IEnumerable<LocationDto> GetLocations(Location location, int maxDistance, int maxResults)
        {
            if (maxResults == 0)
            {
                return new List<LocationDto>();
            }
            var mainTree = GetMainQuadTree();
            var locationDistances = new List<LocationDto>();
            var locationBoundary = location.ToBoundary(maxDistance); 
            var locationsInsideTheBoundary =
                GetAllLocationByBoundary(mainTree.QuadTreeId, locationBoundary, null).ToList();

            foreach (var founded in locationsInsideTheBoundary)
            {
                var distance = founded.CalculateDistance(location);
                locationDistances.Add(new LocationDto(founded.Latitude, founded.Longitude, distance));
            }
            return locationDistances.OrderBy(x => x.Distance).Take(maxResults).ToList();
        }
    }
}
