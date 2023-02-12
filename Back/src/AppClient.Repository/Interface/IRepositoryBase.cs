using AppClient.Repository.Models;
using System.Linq.Expressions;

namespace AppClient.Repository.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity InsertDb(TEntity entity);
        TEntity UpdateDb(TEntity entity);
        TEntity DeleteDb(TEntity entity);
        Task<PageList<TEntity>> GetByWhere(PageParams pageParams);
        TEntity GetById(int id);

        bool SaveChanges();
    }
}
