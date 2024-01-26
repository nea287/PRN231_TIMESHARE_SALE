using Microsoft.EntityFrameworkCore;
using PRN231_TIMESHARE_SALES_DAO.DAO;
using PRN231_TIMESHARE_SALES_DAO.IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_Repository.IRepository
{
    public interface IBaseRepository<TEntity> : IBaseDAO<TEntity> where TEntity : class
    {
    }
}
