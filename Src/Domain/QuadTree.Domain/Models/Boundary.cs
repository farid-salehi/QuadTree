using System.Collections.Generic;

namespace QuadTree.Domain.Models
{
    public class Boundary
    {
        public int BoundaryId { get; set; }
        public int TopLeftId { get; set; }
        public virtual Location TopLeft { get; set; }
        public virtual Location TopRight { get; set; }
        public int TopRightId { get; set; }
        public Location DownLeft { get; set; }
        public int DownLeftId { get; set; }
        public virtual Location DownRight { get; set; }
        public int DownRightId { get; set; }
        public virtual ICollection<QuadTree> QuadTreeCollection { get; set; }
            = new List<QuadTree>();
    }
}
