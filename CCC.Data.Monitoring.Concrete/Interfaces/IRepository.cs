using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CCC.Data.Monitoring.Concrete.Interfaces
{
    public interface IRepository<TEntity> where TEntity : ITable
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate); 
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
}
