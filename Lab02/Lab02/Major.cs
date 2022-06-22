using System;
using System.Collections.Generic;

#nullable disable

namespace Lab02
{
    public partial class Major
    {
        public Major()
        {
            Students = new HashSet<Student>();
        }

        public int Code { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
