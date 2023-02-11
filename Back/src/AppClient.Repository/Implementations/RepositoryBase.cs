using AppClient.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppClient.Repository.Implementations
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        protected DbSet<TEntity> DbSet;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public TEntity InsertDb(TEntity entity)
        => DbSet.Add(entity).Entity;        

        public TEntity UpdateDb(TEntity entity)
        => DbSet.Update(entity).Entity;

        public TEntity DeleteDb(TEntity entity)
        => DbSet.Remove(entity).Entity;

        public bool SaveChanges()
        => _context.SaveChanges() > 0;

        public List<TEntity> GetByWhere(Expression<Func<TEntity, bool>>? where = null)
        => where == null? DbSet.ToList() : DbSet.Where(where).ToList();        
    }
}
