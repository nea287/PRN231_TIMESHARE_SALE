using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class DepartmentOfProject
    {
        public DepartmentOfProject()
        {
            AvailableTimes = new HashSet<AvailableTime>();
        }

        public int DepartmentId { get; set; }
        public int ProjectId { get; set; }
        public string DepartmentProjectCode { get; set; }

        public virtual Department Department { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<AvailableTime> AvailableTimes { get; set; }
    }
}
