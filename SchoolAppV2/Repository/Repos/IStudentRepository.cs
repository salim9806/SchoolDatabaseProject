using DataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        IQueryable<Student> GetAllStudents(Expression<Func<Student, string>> orderBy, bool orderByDescending);
        IQueryable<Student> GetStudentsByClass(string className, Expression<Func<Student, object>> orderBy, bool orderByDescending);
    }
}
