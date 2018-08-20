using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WpfApplication1.Repositories
{
    public interface IGenericRepository<T>
    {
        T Add(T entity);
        void Delete(int id);
        void Update(T entity);
        ICollection<T> List();
        T SearchById(int id);
        ICollection<T> SearchFor(Expression<Func<T, bool>> filter);
    }
}
