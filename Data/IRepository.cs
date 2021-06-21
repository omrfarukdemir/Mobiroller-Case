using Mobiroller.Data.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobiroller.Data
{
    public interface IRepository<in TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        Task AddRange(IEnumerable<TEntity> entities);

        ValueTask<TEntity> GetById(TKey id);

        Task<List<TEntity>> GetAll();

        List<TEntity> FindBy(Func<TEntity, bool> predicate);
    }
}