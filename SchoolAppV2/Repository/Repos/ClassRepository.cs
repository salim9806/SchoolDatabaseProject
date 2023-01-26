using DataAccess;
using DataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class ClassRepository : RepositoryBase<Class>, IClassRepository
    {
        public ClassRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
