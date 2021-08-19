using Product.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.RepositoryInterface
{
    public interface IRepositoryBase<T> where T : class, IData
    {
        IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path, Expression<Func<T, bool>> filter);
        IQueryable<T> Includes(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Filter(Expression<Func<T, bool>> filter);
        public IQueryable<T> Get(Expression<Func<T, bool>> filter);
        IQueryable<T> Get();
        void Add(T entity);
        void Delete(object id);
        void Delete(T entityToDelete);
        void DeleteChild(T entityToUpdate);
        void Update(T entityToUpdate);

        bool Any(Expression<Func<T, bool>> filter);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAsync();
        Task<int> CountAsync();
        Task<List<T>> GetAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAsync(string sort, int skip, int take);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task DeleteAsync(object id);
    }
}
