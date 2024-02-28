using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Distributed;
using MimeKit;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.Text;

namespace PRN231_TIMESHARE_SALES_BusinessLayer.Helpers
{
    public class SupportingFeature
    {
        private static SupportingFeature instance = null;
        private static readonly object InstanceClock = new object();


        public static SupportingFeature Instance
        {
            get
            {
                lock (InstanceClock)
                {
                    if (instance == null)
                    {
                        instance = new SupportingFeature();
                    }
                    return instance;
                }
            }
        }

        public void SendEmail(string receiver, string content, string title)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("tho.kieu@reso.vn", "1phanngocnga");

                using (MimeMessage message = new MimeMessage())
                {
                    message.From.Add(new MailboxAddress("TimeSharing", "timesharing.fpt@gmail.com"));
                    message.To.Add(new MailboxAddress("", receiver));

                    BodyBuilder builder = new BodyBuilder();
                    builder.TextBody = content;

                    message.Subject = title;
                    message.Body = builder.ToMessageBody();

                    client.Send(message);
                }
            }
        }

        public string GenerateCode()
        {
            return Convert.ToString(new Random().Next(100000, 999999));
        }

    }
}
