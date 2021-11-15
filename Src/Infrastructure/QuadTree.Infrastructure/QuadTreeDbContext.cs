using Microsoft.EntityFrameworkCore;
using QuadTree.Domain.Models;

namespace QuadTree.Infrastructure
{
    public class QuadTreeDbContext : DbContext
    {
        public QuadTreeDbContext(DbContextOptions<QuadTreeDbContext> options) : base(options)
        {
        }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Domain.Models.QuadTree> QuadTrees { get; set; }
        public DbSet<Boundary> Boundaries { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region QuadTree
            modelBuilder.Entity<Domain.Models.QuadTree>().HasKey(k => k.QuadTreeId);

            modelBuilder.Entity<Domain.Models.QuadTree>(entity =>
            {
                entity.HasOne(x => x.NorthWest)
                    .WithMany(x => x.NorthWestCollection)
                    .HasForeignKey(x => x.NorthWestId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Domain.Models.QuadTree>(entity =>
            {
                entity.HasOne(x => x.NorthEast)
                    .WithMany(x => x.NorthEastCollection)
                    .HasForeignKey(x => x.NorthEastId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Domain.Models.QuadTree>(entity =>
            {
                entity.HasOne(x => x.SouthWest)
                    .WithMany(x => x.SouthWestCollection)
                    .HasForeignKey(x => x.SouthWestId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Domain.Models.QuadTree>(entity =>
            {
                entity.HasOne(x => x.SouthEast)
                    .WithMany(x => x.SouthEastCollection)
                    .HasForeignKey(x => x.SouthEastId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Domain.Models.QuadTree>(entity =>
            {
                entity.HasOne(x => x.Boundary)
                    .WithMany(x => x.QuadTreeCollection)
                    .HasForeignKey(x => x.BoundaryId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region Location
            modelBuilder.Entity<Location>().HasKey(k => k.LocationId);
            modelBuilder.Entity<Location>().Property(p => p.Address)
                .HasMaxLength(500);
            modelBuilder.Entity<Boundary>().HasMany(e => e.QuadTreeCollection)
                .WithOne(e => e.Boundary)
                .HasForeignKey(e => e.BoundaryId);


            #endregion

            #region Boundary
            modelBuilder.Entity<Boundary>().HasKey(k => k.BoundaryId);

            modelBuilder.Entity<Boundary>(entity =>
            {
                entity.HasOne(x => x.TopLeft)
                    .WithMany(x => x.TopLeftBoundaryCollection)
                    .HasForeignKey(x => x.TopLeftId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<Boundary>(entity =>
            {
                entity.HasOne(x => x.TopRight)
                    .WithMany(x => x.TopRightBoundaryCollection)
                    .HasForeignKey(x => x.TopRightId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<Boundary>(entity =>
            {
                entity.HasOne(x => x.DownLeft)
                    .WithMany(x => x.DownLeftBoundaryCollection)
                    .HasForeignKey(x => x.DownLeftId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Boundary>(entity =>
            {
                entity.HasOne(x => x.DownRight)
                    .WithMany(x => x.DownRightBoundaryCollection)
                    .HasForeignKey(x => x.DownRightId).IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            
            #endregion

        }
    }
}
