using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BluePrintSys.Core.IRepository
{
    public interface IGenericRepository<T> where T : class
    {

        Task<IList<T>> GetAll(
           Expression<Func<T, bool>> expression = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           List<string> includes = null
           );


        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);

        Task<T> GetById(object id);

        Task Insert(T entity);

        Task InsertRange(IEnumerable<T> entities);

        Task Delete(int id);

        void DeleteRange(IEnumerable<T> entities);

        void Update(T entity);


    }


}
