using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.ResponseModels.Helpers
{
    public class UserLoginResponse
    {
        public string? Message { get; set; }
        public AccountViewModel? Value { get; set; }
        public bool? Result { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
