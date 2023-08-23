using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<bool> AnyAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> CheckIdIfExists(Expression<Func<T, bool>> predicate);
        T AddToken(T entity);
        bool CheckTokenIfExist(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> OrderByAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> OrderByDescAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int?>> orderByDesc);
        Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate);

        //Get Single
        Task<T> GetSingle(Expression<Func<T, bool>> predicate);

        //Get Unique
        Task<T> GetUnique(Expression<Func<T, bool>> predicate);
    }
}
