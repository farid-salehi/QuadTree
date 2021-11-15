namespace QuadTree.Domain.DataTransferObjects
{
    public class Settings
    {
        public ushort DefaultQuadTreeCapacity { get; set; }
        public double TopLeftLatitude { get; set; }
        public double TopLeftLongitude { get; set; }
        public double TopRightLatitude { get; set; }
        public double TopRightLongitude { get; set; }
        public double DownLeftLatitude { get; set; }
        public double DownLeftLongitude { get; set; }
        public double DownRightLatitude { get; set; }
        public double DownRightLongitude { get; set; }
    }
}
