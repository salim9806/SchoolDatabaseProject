using DataAccess.DataEntities;

namespace Repository.Repos
{
    public interface IPersonnelRepository:IRepositoryBase<Personnel>
    {
        void AddNewPersonnel(string firstName, string lastName, string department, IEnumerable<string> occupations, DateTime dateStarted);
    }
}