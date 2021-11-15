using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuadTree.Domain.InfrastructureInterfaces;

namespace QuadTree.Infrastructure.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly QuadTreeDbContext DbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(QuadTreeDbContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public virtual async Task<T> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual T Remove(T entity)
        {
            _dbSet.Remove(entity);
            return entity;
        }

        public virtual async Task<T> GetById(object id)
        {
            return await _dbSet.FindAsync(id);

        }


        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities)
        {
            var enumerable = entities.ToList();
            await _dbSet.AddRangeAsync(enumerable);
            return enumerable;
        }

        public virtual IEnumerable<T> RemoveRange(IEnumerable<T> entities)
        {
            var enumerable = entities.ToList();
            _dbSet.RemoveRange(enumerable);
            return enumerable;
        }
    }
}
