using System;
using System.Collections.Generic;

namespace DataAccess.DataEntities
{
    public partial class Occupation
    {
        public Occupation()
        {
            Personnel = new HashSet<Personnel>();
        }

        public string Title { get; set; } = null!;
        public int Salary { get; set; }

        public virtual ICollection<Personnel> Personnel { get; set; }
    }
}
