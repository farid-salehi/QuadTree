using System;
using QuadTree.Domain.Models;

namespace QuadTree.Application.Extensions
{
    internal static class LocationExtensions
    {
        internal static Location FindMiddle(this Location locationA, Location locationB)
        {
            var middleLat = (locationA.Latitude + locationB.Latitude) / 2;
            var middleLong = (locationA.Longitude + locationB.Longitude) / 2;
            return new Location(middleLat, middleLong);
        }

        /// <summary>
        /// Creates a new location that is <paramref name="offsetLat"/>, <paramref name="offsetLon"/> meters from this location.
        /// </summary>
        internal static Location Add(this Location location, double offsetLat, double offsetLon)
        {
            double latitude = location.Latitude + (offsetLat / 111111d);
            double longitude = location.Longitude + (offsetLon / (111111d * Math.Cos(latitude)));

            return new Location(latitude, longitude);
        }

        internal static double CalculateDistance(this Location locationA,Location locationB)
        {
            var rlat1 = Math.PI * locationA.Latitude / 180;
            var rlat2 = Math.PI * locationB.Latitude / 180;
            var rlon1 = Math.PI * locationA.Longitude / 180;
            var rlon2 = Math.PI * locationB.Longitude / 180;
            var theta = locationA.Longitude - locationB.Longitude;
            var rtheta = Math.PI * theta / 180;
            var dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return dist * 1609.344;
        }
    }
}
