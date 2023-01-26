using System;
using System.Collections.Generic;

namespace DataAccess.DataEntities
{
    public partial class Course
    {
        public Course()
        {
            Grades = new HashSet<Grade>();
        }

        public string CourseName { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
    }
}
