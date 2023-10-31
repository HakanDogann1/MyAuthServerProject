using System.Linq.Expressions;

namespace MyAuthServerProject.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Remove(TEntity entity);
        TEntity Update(TEntity entity);

    }
}
