using System;
using System.Collections.Generic;

#nullable disable

namespace Lab02
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Male { get; set; }
        public DateTime? Dob { get; set; }
        public int? Major { get; set; }
        public bool? Active { get; set; }
        public int? Scholarship { get; set; }

        public virtual Major MajorNavigation { get; set; }
    }
}
