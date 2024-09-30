using Domain.Entities.Common;
using Domain.Interfaces;
using Iinfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Iinfrastructure.Repositories.Common
{
    public abstract class BaseRepository<T>(LibraryContext context) : IRepository<T> where T : BaseEntity
    {
        private readonly LibraryContext _context = context;

        public virtual async Task AddEntityAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual async Task<ICollection<T>> GetEntitiesByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<ICollection<T>> GetAllEntitiesAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetEntityByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<T?> GetEntityByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual void UpdateEntity(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<T,bool>> predicate)
        {
            return await _context.Set<T>().CountAsync(predicate);
        }


    }

}