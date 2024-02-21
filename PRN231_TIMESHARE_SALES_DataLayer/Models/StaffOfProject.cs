using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public class StaffOfProject
    {
        public StaffOfProject()
        {
            //Accounts = new HashSet<Account>();
            //Projects = new HashSet<Project>();
        }
        public int StaffId { get; set; }
        public int ProjectId { get; set; }  
        //public virtual ICollection<Account> Accounts { get; set; }
        //public virtual ICollection<Project> Projects { get; set; }
        public virtual Account Account { get; set; }
        public virtual Project Project { get; set; }
    }
}
