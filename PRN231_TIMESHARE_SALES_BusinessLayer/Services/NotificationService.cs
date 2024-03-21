using PRN231_TIMESHARE_SALES_BusinessLayer.Helpers;
using PRN231_TIMESHARE_SALES_BusinessLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Services
{
    public class NotificationService : INotificationService
    {
        public bool SendMailNotification(string email, string content, string title)
        {
            try
            {
                SupportingFeature.Instance.SendEmail(email, content, title);
            }catch(Exception ex)
            {
                return false;
            }

            return true;
            
        }
    }
}
