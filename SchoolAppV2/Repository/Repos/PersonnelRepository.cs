using DataAccess;
using DataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class PersonnelRepository : RepositoryBase<Personnel>, IPersonnelRepository
    {
        public PersonnelRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void AddNewPersonnel(string firstName, string lastName, string department, IEnumerable<string> occupations, DateTime dateStarted)
        {
            Personnel newPersonnel = new Personnel { FirstName = firstName, LastName = lastName, StartedDate = dateStarted };

            newPersonnel.WorksInDepartment = department;

            foreach (Occupation occupation in _dbContext.Occupations.Where(o => occupations.Any(po => po == o.Title)).ToList())
                newPersonnel.OccupationTitles.Add(occupation);

            _dbSet.Add(newPersonnel);
        }
    }
}
