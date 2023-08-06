using Domain.Entities.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.Base
{
    public interface IRepository <T> where T : BaseEntity
    {
        Task<PaginationResult<T>> GetAsync(
                Expression<Func<T, bool>> expression = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                bool isDisableTracking = true,
                bool isTakeAll = false,
                int pageSize = 0,
                int pageIndex = 0);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);

        Task<T> AddAsync(T entity);
        T UpdateAsync(T entity);  
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteAsync(object id);
    }
}
