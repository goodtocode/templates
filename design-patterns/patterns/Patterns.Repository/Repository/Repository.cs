using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodToCode.Templates.Patterns.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly GenericDbContext<T> _context;
        private bool disposed = false;

        public Repository(IGenericDbContext<T> context)
        {
            _context = (GenericDbContext<T>)context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Items.ToList();
        }

        public IEnumerable<T> GetByWhere(Func<T, bool> queryPredicate)
        {
            return _context.Items.Where(queryPredicate);
        }

        public T GetByKey(Guid key)
        {
            return _context.Items.Find(key);
        }

        public async Task<int> CreateAsync(T item)
        {
            _context.Items.Add(item);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected;
        }

        public async Task<int> UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected;
        }

        public async Task<int> DeleteAsync(Guid key)
        {
            var item = GetByKey(key);
            _context.Remove(item);
            _context.Entry(item).State = EntityState.Deleted;
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}