using Microsoft.EntityFrameworkCore;
using PRN231_TIMESHARE_SALES_DAO.IDAO;
using PRN231_TIMESHARE_SALES_DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231_TIMESHARE_SALES_DAO.DAO
{
    public class BaseDAO<TEntity> : IBaseDAO<TEntity> where TEntity : class
    {
        private static BaseDAO<TEntity> instance = null;
        private static readonly object InstanceClock = new object();

        private readonly TimeshareSalesDBContext _context;
        private DbSet<TEntity> Table { get; set; }
        
        public static BaseDAO<TEntity> Instance
        {
            get
            {
                lock (InstanceClock)
                {
                    if (instance == null)
                    {
                        TimeshareSalesDBContext context = new TimeshareSalesDBContext();
                        instance = new BaseDAO<TEntity>(context);
                    }
                    return instance;
                }
            }
        } 

        public BaseDAO(TimeshareSalesDBContext context)
        {
            _context = context;
            Table = context.Set<TEntity>();
        }
        
        public virtual bool Any(Func<TEntity, bool> predicate)
        {
            return Table.Any(predicate);
        }

        public virtual int Count(Func<TEntity, bool> predicate)
        {
            return Table.Where(predicate).Count();
        }

        public virtual void DeleteRange(IQueryable<TEntity> entities)
        {
            Table.RemoveRange(entities);
        }

        public virtual TEntity Find(Func<TEntity, bool> predicate)
        {
            return Table.Find(predicate);
        }

        public virtual IQueryable<TEntity> FindAll(Func<TEntity, bool> predicate)
        {
            return Table.Where(predicate).AsQueryable();
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.FindAsync(predicate);
        }

        public virtual DbSet<TEntity> GetAll()
        {
            return Table;
        }

        public virtual IEnumerable<TEntity> GetAll(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = null)
        {
            IQueryable<TEntity> query = Table;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<TEntity> GetByIdGuid(Guid id)
        {
            return await Table.FindAsync(id);
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<TEntity> query = Table;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();
        }

        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.Where(predicate).ToListAsync();
        }

        public async Task HardDelete(int key)
        {
            var rs = await GetById(key);
            Table.Remove(rs);
        }

        public async Task HardDeleteGuid(Guid key)
        {
            var rs = await GetByIdGuid(key);
            Table.Remove(rs);
        }

        public void Insert(TEntity entity)
        {
            Table.Add(entity);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
        }

        public async void InsertRangeAsync(IQueryable<TEntity> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public void SaveChages()
        {
            _context.SaveChanges();
        }

        public async Task UpdateById(TEntity entity, int id)
        {
            var existedEntity = await GetById(id);
            _context.Entry(existedEntity).CurrentValues.SetValues(entity);
            Table.Update(existedEntity);
        }

        public async Task UpdateGuid(TEntity entity, Guid id)
        {
            var existedEntity = await GetByIdGuid(id);
            _context.Entry(existedEntity).CurrentValues.SetValues(entity);
            Table.Update(entity);
        }

        public void UpdateRange(IQueryable<TEntity> entities)
        {
            Table.UpdateRange(entities);
        }

        public TEntity FistOrDefault(Func<TEntity, bool> predicate)
        {
            return Table.FirstOrDefault(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }

        public bool IsMin(Func<TEntity, bool> predicate)
        {
            return Table.Min(predicate);
        }

        public bool IsMax(Func<TEntity, bool> predicate)
        {
            return Table.Max(predicate);
        }

        public TEntity GetMin()
        {
            return Table.Min();
        }

        public TEntity GetMax()
        {
            return Table.Max();
        }

        public async Task<bool> IsMaxAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.MaxAsync(predicate);
        }

        public async Task<bool> IsMinAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.MinAsync(predicate);
        }

        public TEntity GetMin(Func<TEntity, bool> predicate)
        {
            return Table.MinBy(predicate);
        }

        public TEntity GetMax(Func<TEntity, bool> predicate)
        {
            return Table.MaxBy(predicate);
        }

        public int Count()
        {
            return Table.Count();
        }

        public virtual void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }
    }
}
