
using System.Linq.Expressions;

namespace OnlineStory.Domain.Abstractions.RepositoryBase;

public interface IGenericRepository<TEntity,in TKey> where TEntity : class
{
    Task<TEntity?> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    void RemoveMultiple(List<TEntity> entities);
    /// <summary>
    /// Executes a raw SQL query and returns the result as an <see cref="IQueryable{T}"/>.
    /// </summary>
    /// <param name="sql">The raw SQL query to execute.</param>
    /// <param name="parameters">The parameters for the SQL query.</param>
    /// <returns>An <see cref="IQueryable{T}"/> of entities returned by the query.</returns>
    IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters);

}
public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    void RemoveMultiple(List<TEntity> entities);
    /// <summary>
    /// Executes a raw SQL query and returns the result as an <see cref="IQueryable{T}"/>.
    /// </summary>
    /// <param name="sql">The raw SQL query to execute.</param>
    /// <param name="parameters">The parameters for the SQL query.</param>
    /// <returns>An <see cref="IQueryable{T}"/> of entities returned by the query.</returns>
    IQueryable<TEntity> FromSqlRaw(string sql, params object[] parameters);

}