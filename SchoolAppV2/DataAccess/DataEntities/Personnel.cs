using System;
using System.Collections.Generic;

namespace DataAccess.DataEntities
{
    public partial class Personnel
    {
        public Personnel()
        {
            Grades = new HashSet<Grade>();
            OccupationTitles = new HashSet<Occupation>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string WorksInDepartment { get; set; } = null!;
        public DateTime StartedDate { get; set; }

        public virtual Department WorksInDepartmentNavigation { get; set; } = null!;
        public virtual ICollection<Grade> Grades { get; set; }

        public virtual ICollection<Occupation> OccupationTitles { get; set; }
    }
}
