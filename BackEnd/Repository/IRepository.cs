using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace BackEnd.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById<TId>(TId id);
        Task<List<T>> ListAll();
        Task<T> Insert(T entity);
        Task Delete(T entity);
        Task<T> Update(T entity);
        Task<List<T>> ListAsyncSpec(ISpecification<T> spec);
        Task<T> GetAsyncSpec(ISpecification<T> spec);
        Task<List<T>> Get(Expression<Func<T, bool>> filter = null,
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                          params Expression<Func<T, object>>[] includeProperties);
    }
}
