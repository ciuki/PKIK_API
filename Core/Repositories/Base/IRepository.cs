using Core.Specifications.Base;
using System.Linq.Expressions;

namespace Core.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetByLambdaAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetBySpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllBySpecAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();

    }
}
