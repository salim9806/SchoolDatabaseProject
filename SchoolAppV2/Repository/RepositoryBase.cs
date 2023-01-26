using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public class RepositoryBase<T>:IRepositoryBase<T> where T: class
    {
        protected ApplicationDbContext _dbContext;
        protected DbSet<T> _dbSet;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>> predicate, bool track=false)
        {
            if(!track)
                return _dbSet.AsNoTracking().Where(predicate);

            return _dbSet.Where(predicate);
        }

        public T? GetById(params object[] ids)
        {
            return _dbSet.Find(ids);
        }
    }
}