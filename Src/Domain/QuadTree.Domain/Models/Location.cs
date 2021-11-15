
using System.Collections.Generic;

namespace QuadTree.Domain.Models
{
    public class Location
    {
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public int LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address{ get; set; }
        public virtual ICollection<Boundary> TopLeftBoundaryCollection { get; set; }
        = new List<Boundary>();
        public virtual ICollection<Boundary> TopRightBoundaryCollection { get; set; }
      = new List<Boundary>();
        public virtual ICollection<Boundary> DownLeftBoundaryCollection { get; set; }
        public virtual ICollection<Boundary> DownRightBoundaryCollection { get; set; }
      = new List<Boundary>();
        public int? QuadTreeId { get; set; }
        public virtual QuadTree QuadTree { get; set; }
    }
}
