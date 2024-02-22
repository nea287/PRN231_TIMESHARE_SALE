﻿using PRN231_TIMESHARE_SALES_DAO.IDAO;
using PRN231_TIMESHARE_SALES_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_DAO.DAO
{
    public class FacilityDAO : BaseDAO<Facility>, IFacilityDAO
    {
        public FacilityDAO(PRN231_TimeshareSalesDBContext context) : base(context)
        {
        }
    }
}
