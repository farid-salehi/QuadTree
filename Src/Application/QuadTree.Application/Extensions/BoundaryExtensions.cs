using QuadTree.Domain.Models;

namespace QuadTree.Application.Extensions
{
    internal static class BoundaryExtensions
    {
        internal static bool AtRange(this Boundary boundary, Location location)
        {
            return (location.Latitude >= boundary.DownRight.Latitude
                    && location.Latitude <= boundary.TopRight.Latitude
                    && location.Longitude >= boundary.TopLeft.Longitude
                    && location.Longitude <= boundary.TopRight.Longitude);
        }

        internal static bool Intersects(this Boundary quadTreeBoundary, Boundary range)
        {
            var rangeIsIn = (quadTreeBoundary.AtRange(range.TopLeft)
                    || quadTreeBoundary.AtRange(range.TopRight)
                    || quadTreeBoundary.AtRange(range.DownLeft)
                    || quadTreeBoundary.AtRange(range.DownRight));

            if (rangeIsIn)
            {
                return true;
            }
            var rangeIsBigger = (range.AtRange(quadTreeBoundary.TopLeft)
                                 && range.AtRange(quadTreeBoundary.TopRight)
                                 && range.AtRange(quadTreeBoundary.DownLeft)
                                 && range.AtRange(quadTreeBoundary.DownRight));
            return rangeIsBigger;

        }

        internal static Boundary ToBoundary(this Location location, int distance)
        {
            var latPlus = location.Add(distance, 0);
            var latMinus = location.Add(-1 * distance, 0);
            var longPlus = location.Add(0, distance);
            var longMinus = location.Add(0, -1 * distance);
            

            return new Boundary()
            {
                TopLeft = new Location(latPlus.Latitude, longMinus.Longitude),
                TopRight = new Location(latPlus.Latitude, longPlus.Longitude),
                DownLeft = new Location(latMinus.Latitude, longMinus.Longitude),
                DownRight = new Location(latMinus.Latitude, longPlus.Longitude),
            };
        }
    }
}
