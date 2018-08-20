using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WpfApplication1.DataAccess;

namespace WpfApplication1.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationContext _context;

        protected DbSet<T> _dbSet;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual ICollection<T> List()
        {
            return _dbSet.ToList();
        }

        public T SearchById(int id)
        {
            return _dbSet.Find(id);
        }

        public ICollection<T> SearchFor(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter).ToList();
        }
    }
}