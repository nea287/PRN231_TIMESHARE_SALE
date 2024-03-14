using System;
using System.Collections.Generic;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class Owner
    {
        public Owner()
        {
            Departments = new HashSet<Department>();
        }

        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
