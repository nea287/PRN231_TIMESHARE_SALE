using PRN231_TIMESHARE_SALES_DAO.IDAO;
using PRN231_TIMESHARE_SALES_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_DAO.DAO
{
    public class CustomerRequestDAO : BaseDAO<CustomerRequest>, ICustomerRequestDAO
    {
        public CustomerRequestDAO(TimeshareSalesDBContext context) : base(context)
        {
        }
    }
}
