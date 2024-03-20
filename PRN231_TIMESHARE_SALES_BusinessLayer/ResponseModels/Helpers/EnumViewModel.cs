using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers
{
    public class EnumViewModel
    {
        public string Message { get; set; }
        public Dictionary<int, string> Results { get; set; }
    }
}
