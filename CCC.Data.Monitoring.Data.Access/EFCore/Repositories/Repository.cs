using CCC.Data.Monitoring.Concrete.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CCC.Data.Monitoring.Data.Access.EFCore.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class , ITable
    {
        protected readonly MonitoringDbContext Context;

        public Repository(MonitoringDbContext context)
        {
            Context = context;
        }
        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
            Context.SaveChanges();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        } 
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }
        
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            Context.UpdateRange(entities);
            Context.SaveChanges();
        }
    }
}
