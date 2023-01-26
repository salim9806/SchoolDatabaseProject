using System.Linq.Expressions;

namespace Repository
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllWhere(Expression<Func<T, bool>> predicate, bool track = false);

        T? GetById(params object[] ids);
        void Commit();
    }
}