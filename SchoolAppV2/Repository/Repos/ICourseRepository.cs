using DataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public interface ICourseRepository:IRepositoryBase<Course>
    {
        IQueryable<Course> GetActiveCourses();
    }
}
