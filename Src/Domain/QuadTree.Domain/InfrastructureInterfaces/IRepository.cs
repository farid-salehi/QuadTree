using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuadTree.Domain.InfrastructureInterfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Add(T entity);
        T Remove(T entity);
        Task<T> GetById(object id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);
        IEnumerable<T> RemoveRange(IEnumerable<T> entities);
    }
}
