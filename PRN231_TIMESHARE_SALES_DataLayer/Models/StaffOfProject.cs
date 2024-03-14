using System.Text.Json.Serialization;

namespace PRN231_TIMESHARE_SALES_DataLayer.Models
{
    public partial class StaffOfProject
    {
        public int StaffId { get; set; }
        public int ProjectId { get; set; }
        //[JsonIgnore]
        public virtual Project Project { get; set; }
        //[JsonIgnore]
        public virtual Account Staff { get; set; }
    }
}