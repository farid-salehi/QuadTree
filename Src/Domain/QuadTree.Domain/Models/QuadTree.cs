using System.Collections.Generic;

namespace QuadTree.Domain.Models
{
    public class QuadTree
    {
        public int QuadTreeId { get; set; }
        public ushort Capacity { get; set; }
        public virtual Boundary Boundary { get; set; }

        public int BoundaryId { get; set; }
        public bool Divided { get; set; } = false;

        public virtual int? NorthWestId { get; set; }
        public virtual QuadTree NorthWest { get; set; }
        public virtual ICollection<QuadTree> NorthWestCollection { get; set; }
       = new List<QuadTree>();

        public virtual int? NorthEastId { get; set; }
        public virtual QuadTree NorthEast { get; set; }
        public virtual ICollection<QuadTree> NorthEastCollection { get; set; }
      = new List<QuadTree>();

        public virtual int? SouthWestId { get; set; }
        public virtual QuadTree SouthWest { get; set; }
        public virtual ICollection<QuadTree> SouthWestCollection { get; set; }
       = new List<QuadTree>();

        public virtual int? SouthEastId { get; set; }
        public virtual QuadTree SouthEast { get; set; }
        public virtual ICollection<QuadTree> SouthEastCollection { get; set; }
       = new List<QuadTree>();
        public virtual ICollection<Location> Locations { get; set; }
          = new List<Location>();

    }
}
