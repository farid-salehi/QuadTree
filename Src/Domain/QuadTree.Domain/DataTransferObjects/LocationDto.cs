
namespace QuadTree.Domain.DataTransferObjects
{
    public class LocationDto
    {
        public LocationDto(double latitude, double longitude, double distance)
        {
            Latitude = latitude;
            Longitude = longitude;
            Distance = distance;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
    }
}
