using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AppClient.Domain.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity InsertDb(TEntity entity);
        TEntity UpdateDb(TEntity entity);
        TEntity DeleteDb(TEntity entity);
        List<TEntity> GetByWhere(Expression<Func<TEntity, bool>>? predicate = null);
        bool SaveChanges();
    }
}
