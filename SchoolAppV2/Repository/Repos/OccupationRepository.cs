using DataAccess;
using DataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class OccupationRepository : RepositoryBase<Occupation>, IOccupationRepository
    {
        public OccupationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
