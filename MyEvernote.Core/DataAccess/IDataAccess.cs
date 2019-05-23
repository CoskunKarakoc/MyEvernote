using MyEvernote.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyEvernote.Core.DataAccess
{
    public interface IDataAccess<T> where T:class,IEntity,new ()
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
