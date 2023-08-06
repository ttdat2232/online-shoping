using Domain.Entities.Base;
using Domain.Interfaces.Repositories.Base;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
             var result = await _dbContext.Set<T>().AddAsync(entity);
            return result.Entity;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null)
        {
            return expression == null ? await _dbContext.Set<T>().CountAsync() : await _dbContext.Set<T>().CountAsync(expression);
        }

        public async Task<T> DeleteAsync(T entity)
        {
            entity = await _dbContext.Set<T>().FindAsync(entity) ?? throw new Exception("Not found");
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.Entry(entity).State = EntityState.Deleted;
            return entity;
        }

        public async Task<T> DeleteAsync(object id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id) ?? throw new Exception("Not found");
            _dbContext.Entry(entity).State = EntityState.Deleted;
            return entity;
        }

        public async Task<PaginationResult<T>> GetAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool isDisableTracking = true, bool isTakeAll = false, int pageSize = 0, int pageIndex = 0)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            var paginationResult = new PaginationResult<T>();
            paginationResult.TotalCount = await CountAsync(expression);
            if (expression != null)
                query = query.Where(expression);
            if(isDisableTracking is true)
                query = query.AsNoTracking();
            if (isTakeAll is true)
            {
                if(orderBy != null)
                    paginationResult.Result = await orderBy(query).ToListAsync();
                else
                    paginationResult.Result = await query.ToListAsync();
            }
            else
            {
                paginationResult.PageIndex = pageIndex;
                if (orderBy == null)
                    paginationResult.Result = await query.Skip(pageSize * pageIndex).Take(pageSize).ToListAsync();
                else
                    paginationResult.Result = await orderBy(query).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            }
            return paginationResult;
        }

        public T UpdateAsync(T entity)
        {
            if(_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            return entity;
        }
    }
}
