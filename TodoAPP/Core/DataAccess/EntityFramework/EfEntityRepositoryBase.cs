using Core.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()

    {
        protected readonly TContext _context;

        public EfEntityRepositoryBase(TContext context)
           => _context = context;

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = _context.Set<TEntity>().Add(entity).Entity;
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return result;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            var result = _context.Set<TEntity>().Remove(entity).Entity;
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return result;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
            => _context.Set<TEntity>().SingleOrDefault(filter)!;


        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
            => filter == null
             ? _context.Set<TEntity>().ToList()
             : _context.Set<TEntity>().Where(filter).ToList();

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
            => _context.Set<TEntity>().SingleOrDefaultAsync(expression)!;

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression = null)
            => expression == null
             ? await _context.Set<TEntity>().CountAsync()
             : await _context.Set<TEntity>().CountAsync(expression);

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null)
            => expression == null
             ? await _context.Set<TEntity>().AsNoTracking().ToListAsync()
             : await _context.Set<TEntity>().AsNoTracking().Where(expression).ToListAsync();

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                context.Dispose();
            }

        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();

                context.Dispose();
                return updatedEntity.Entity;
            }
        }
    }
}
