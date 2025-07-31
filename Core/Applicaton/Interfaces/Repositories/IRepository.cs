using System.Linq.Expressions;

namespace Applicaton.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SoftDeleteAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
    Task<T?> GetByIdAsync(int Id);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

}
