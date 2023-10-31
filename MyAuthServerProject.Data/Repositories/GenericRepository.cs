using Microsoft.EntityFrameworkCore;
using MyAuthServerProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthServerProject.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entities;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var values = await _entities.ToListAsync();
            return values;
        }

        public IQueryable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            var values = _entities.Where(filter);
            return values;

        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var value =await _entities.FindAsync(id);
            if(value == null)
            {
                _context.Entry(value).State = EntityState.Detached;
            }
            return value;
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return entity;
        }
    }
}
