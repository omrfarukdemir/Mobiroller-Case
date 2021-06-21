using Microsoft.EntityFrameworkCore;
using Mobiroller.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobiroller.Data
{
    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        private readonly MobirollerContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(MobirollerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public Task AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);

            return _context.SaveChangesAsync();
        }

        public ValueTask<TEntity> GetById(TKey id)
        {
            return _dbSet.FindAsync(id);
        }

        public Task<List<TEntity>> GetAll()
        {
            return _dbSet.ToListAsync();
        }

        public List<TEntity> FindBy(Func<TEntity, bool> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }
    }
}