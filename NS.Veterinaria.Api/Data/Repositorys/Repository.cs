using Microsoft.EntityFrameworkCore;
using NS.Veterinary.Api.Models;
using NS.Veterinary.Api.Data.Context;
using NS.Veterinary.Api.Interfaces;
using System.Linq.Expressions;

namespace NS.Veterinary.Api.Data.Repositorys
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly VeterinaryContext _context;
        protected readonly DbSet<TEntity> _table;
        protected Repository(VeterinaryContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _table.AsNoTracking().ToListAsync();

        public async Task<TEntity> GetByIdAsync(Guid id)
            => await _table.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id);

        public async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate)
            => await _table.AsNoTracking().Where(predicate).ToListAsync();        

        public async Task RegisterAsync(TEntity entity)
            => await _table.AddAsync(entity);

        public void Update(TEntity entity)
            => _table.Update(entity);

        public void Delete(TEntity entity)
            => _table.Remove(entity);

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
