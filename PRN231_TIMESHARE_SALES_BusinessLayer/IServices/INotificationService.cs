using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.IServices
{
    public interface INotificationService
    {
        public bool SendMailNotification(string email, string content, string title);

    }
}
