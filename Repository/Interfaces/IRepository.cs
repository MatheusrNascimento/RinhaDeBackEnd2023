using System.Linq.Expressions;

namespace RinhaDeBackEnd2023.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task InsertAsync(T entity);
        Task UpdateAsync(Expression<Func<T, bool>> filter, T entity);
        Task DeleteAsync(Expression<Func<T, bool>> filter);
        Task<T> FindOneAsync(Expression<Func<T, bool>> filter);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> filter = null);
    }
}

