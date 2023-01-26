using DataAccess;
using DataAccess.DataEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Student> GetAllStudents(Expression<Func<Student, string>> orderBy, bool orderByDescending)
        {
            var result = GetAll().AsNoTracking().OrderBy(orderBy);

            if (orderByDescending)
                result = GetAll().AsNoTracking().OrderByDescending(orderBy);

            return result.AsQueryable();
        }

        public IQueryable<Student> GetStudentsByClass(string className, Expression<Func<Student, object>> orderBy, bool orderByDescending)
        {
            var result = _dbSet.AsNoTracking().OrderBy(orderBy);

            if (orderByDescending)
                result = _dbSet.AsNoTracking().OrderByDescending(orderBy);

            return result.Where(s => s.BelongToClass == className);
        }
    }
}
