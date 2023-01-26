using System;
using System.Collections.Generic;

namespace DataAccess.DataEntities
{
    public partial class Department
    {
        public Department()
        {
            Personnel = new HashSet<Personnel>();
        }

        public string DepartmentName { get; set; } = null!;

        public virtual ICollection<Personnel> Personnel { get; set; }
    }
}
