using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.API.Data;
using WorkoutTracker.Domain.Repositories.Interfaces;

namespace WorkoutTracker.Domain.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal WorkoutDbContext context { get; set; }
        protected DbSet<TEntity> dbSet { get; set; }

        public GenericRepository(WorkoutDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }
        public TEntity GetById<TId>(TId id)
        {
            return dbSet.Find(id);
        }

        public TEntity GetById<TId>(TId id1, TId id2)
        {
            return dbSet.Find(id1, id2);
        }
        public TEntity Add(TEntity obj)
        {
            dbSet.Add(obj);
            return obj;
        }
        public void Update(Guid id, TEntity obj)
        {
            var existingEntity = dbSet.Find(id);
            context.Entry(existingEntity).CurrentValues.SetValues(obj);
            context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            TEntity existing = dbSet.Find(id);
            dbSet.Remove(existing);
        }
        public void Delete(Guid id1, Guid id2)
        {
            TEntity existing = dbSet.Find(id1, id2);
            dbSet.Remove(existing);
        }
        public IEnumerable<TEntity> Search(Func<TEntity, bool> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
            return entities;
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
