using Microsoft.EntityFrameworkCore;
using Reuse.Bll.Repository.Interface;
using Reuse.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reuse.Bll.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        
        private readonly AppDbContext _context;

        public Repository(AppDbContext appDbContext)
        {
            _context = appDbContext;

        }
        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public T AddToken(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<bool> AnyAsync()
        {
            return await _context.Set<T>().AnyAsync();
        }

        public async Task<bool> CheckIdIfExists(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public bool CheckTokenIfExist(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleAsync(predicate);
        }

        public Task<T> GetUnique(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> OrderByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().OrderBy(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> OrderByDescAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int?>> orderByDesc)
        {
            return await _context.Set<T>().Where(predicate).OrderByDescending(orderByDesc).ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
    }
}
