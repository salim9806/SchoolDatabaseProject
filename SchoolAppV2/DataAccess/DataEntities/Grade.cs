using System;
using System.Collections.Generic;

namespace DataAccess.DataEntities
{
    public partial class Grade
    {
        public string Rating { get; set; } = null!;
        public int StudentId { get; set; }
        public string TakenCourse { get; set; } = null!;
        public int AppointedBy { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Personnel AppointedByNavigation { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
        public virtual Course TakenCourseNavigation { get; set; } = null!;
    }
}
