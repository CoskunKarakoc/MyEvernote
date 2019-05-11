using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entities.Abstract;

namespace MyEvernote.DataAccessLayer.EntityFramework.Abstract
{
    public interface IRepository<T> where T:class,IEntity,new ()
    {
        T Find(Expression<Func<T, bool>> where);
        List<T> List();
        List<T> List(Expression<Func<T, bool>> where);
        int Save();
        int Insert(T obj);
        int Delete(T obj);
        int Update(T obj);
        IQueryable<T> ListQueryable();


    }
}
