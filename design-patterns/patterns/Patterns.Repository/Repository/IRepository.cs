using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Templates.Patterns.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByWhere(Func<T, bool> queryPredicate);
        T GetByKey(Guid key);
        Task<int> CreateAsync(T item);
        Task<int> UpdateAsync(T item);
        Task<int> DeleteAsync(Guid key);
    }
}
