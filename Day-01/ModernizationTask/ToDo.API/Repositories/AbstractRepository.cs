using Microsoft.EntityFrameworkCore;
using ToDo.API.Contexts;
using ToDo.API.Exceptions;
using ToDo.API.Interfaces.Repositories;

namespace ToDo.API.Repositories
{
    public abstract class AbstractRepository<K, T> : IRepository<K, T> where T : class
    {
        protected readonly ToDoContext _context;
        protected readonly DbSet<T> _dbSet;

        public AbstractRepository(ToDoContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async virtual Task<T> Add(T item)
        {
            try
            {
                var result = await _dbSet.AddAsync(item);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (DbUpdateException)
            {
                throw new UnableToAddException();
            }
        }

        public virtual async Task<T> GetById(K key) => await _dbSet.FindAsync(key);

        public async virtual Task<T> Update(T item)
        {
            _dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async virtual Task<T> Delete(K key)
        {
            T result = await GetById(key);
            if (result == null)
            {
                throw new EntityNotFoundException();
            }
            _dbSet.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async virtual Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
