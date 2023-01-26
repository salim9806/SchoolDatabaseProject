using DataAccess;
using DataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Course> GetActiveCourses()
        {
            return GetAllWhere(course => course.IsActive);
        }
    }
}
