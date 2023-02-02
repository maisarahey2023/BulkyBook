using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T-Category
        //return list of category
        IEnumerable<T> GetAll(string? includeProperties = null);
        //return void with parameter category
        void Add(T entity);
        //return category using id
        T Find(int id);
        //return category with paremeter expression like u=>u, id=id
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
        //update should not be in generic repository
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }

}
