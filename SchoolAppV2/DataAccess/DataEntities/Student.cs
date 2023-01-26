using System;
using System.Collections.Generic;

namespace DataAccess.DataEntities
{
    public partial class Student
    {
        public Student()
        {
            Grades = new HashSet<Grade>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string BelongToClass { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public decimal SocialSecurity { get; set; }

        public virtual Class BelongToClassNavigation { get; set; } = null!;
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
