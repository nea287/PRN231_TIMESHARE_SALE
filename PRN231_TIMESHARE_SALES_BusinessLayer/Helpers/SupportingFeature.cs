
using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Distributed;
using MimeKit;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.Text;
using System.Collections;
using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json.Serialization;
using PRN231_TIMESHARE_SALES_BusinessLayer.Commons;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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

        public static IEnumerable<string> GetNameOfProperties<T>()
        {

            return typeof(T).GetProperties().Select(p => p.Name);
        }

        public static IEnumerable<string> GetNameIncludedProperties<T>()
        {
            return typeof(T).GetProperties()
                .Where(x => x.PropertyType.IsGenericType)
                .Where(x => x.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                .Select(p => p.Name);
        }

        public static Expression<Func<TEntity, TKey>> GetPropertyExpression<TEntity, TKey>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(object), "p");
            var property = Expression.Property(Expression.Convert(parameter, typeof(TEntity)), propertyName);
            var convert = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<TEntity, TKey>>(convert, parameter);
        }

        public static Type GetTypeProperty<T>(string propertyName)
        {
            return typeof(T).GetProperties().First(p => p.Name.Equals(propertyName)).PropertyType;
        }

        public static IEnumerable<T> Sorting<T>(IEnumerable<T> searchResult, SortOrder sortType, string colName)
        {
            if (sortType == SortOrder.Ascending)
            {
                return searchResult.OrderBy(item => typeof(T).GetProperties().First(x => x.Name.Equals(colName, StringComparison.CurrentCultureIgnoreCase)).GetValue(item)).AsQueryable();
            }
            else if (sortType == SortOrder.Descending)
            {
                return searchResult.OrderByDescending(item => typeof(T).GetProperties().First(x => x.Name.Equals(colName, StringComparison.CurrentCultureIgnoreCase)).GetValue(item)).AsQueryable();
            }
            else
            {
                return searchResult;
            }
        }


        public string? GetDisplayNameForProperty(PropertyInfo property, string otherProperty)
        {
            IEnumerable<Attribute> attributes = CustomAttributeExtensions.GetCustomAttributes(property, true);
            foreach (Attribute attribute in attributes)
            {
                if (attribute is DisplayAttribute display)
                {
                    return display.GetName();
                }
            }

            return otherProperty;
        }

        public Dictionary<int, string> GetEnumName<TEnum>()
        {
            Dictionary<int, string> enumValues = new Dictionary<int, string>();

            foreach (int e in Enum.GetValues(typeof(TEnum)))
            {
                enumValues.Add(e, Enum.GetName(typeof(TEnum), e));
            }

            return enumValues;  
        }
    }
}
