using NS.Veterinaria.Api.Models;
using System.Linq.Expressions;

namespace NS.Veterinary.Api.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate);
        Task RegisterAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
