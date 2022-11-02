using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Domain.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById<TId>(TId id1, TId id2);
        TEntity GetById<TId>(TId id);
        TEntity Add(TEntity obj);
        void Update(Guid id, TEntity obj);
        void Delete(Guid id);
        void Delete(Guid id1, Guid id2);
        void Save();
        IEnumerable<TEntity> Search(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
