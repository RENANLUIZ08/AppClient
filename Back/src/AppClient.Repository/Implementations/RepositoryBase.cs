using AppClient.Repository.Interfaces;
using AppClient.Repository.Models;
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

        public async Task<PageList<TEntity>> GetByWhere(PageParams pageParams, Expression<Func<TEntity, bool>>? where = null)
         => await PageList<TEntity>.CreateAsync(
            where != null
            ? DbSet.Where(where)
            : DbSet.AsQueryable(),
            pageParams.PageNumber,
            pageParams.PageSize
            );

        public TEntity GetById(int id)
         => DbSet.Find(id);
    }
}
