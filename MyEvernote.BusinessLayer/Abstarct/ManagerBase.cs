using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyEvernote.Core.DataAccess;
using MyEvernote.Core.Entities;
using MyEvernote.DataAccessLayer.EntityFramework.Concrete;

namespace MyEvernote.BusinessLayer.Abstarct
{
    public abstract class ManagerBase<T> : IDataAccess<T> where T : class, IEntity, new()
    { 
       
        Repository<T> _repository=new Repository<T>();

        public T Find(Expression<Func<T, bool>> @where)
        {
           return _repository.Find(where);
        }

        public List<T> List()
        {
            return _repository.List();
        }

        public List<T> List(Expression<Func<T, bool>> @where)
        {
            return _repository.List(where);
        }

        public int Save()
        {
            return _repository.Save();
        }

        public int Insert(T obj)
        {
            return Insert(obj);
        }

        public int Delete(T obj)
        {
            return _repository.Delete(obj);
        }

        public int Update(T obj)
        {
            return _repository.Update(obj);
        }

        public IQueryable<T> ListQueryable()
        {
            return _repository.ListQueryable();
        }
    }
}
