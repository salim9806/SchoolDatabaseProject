using System;
using System.Collections.Generic;

namespace DataAccess.DataEntities
{
    public partial class Class
    {
        public Class()
        {
            Students = new HashSet<Student>();
        }

        public string ClassName { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
    }
}
