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
       
       private Repository<T> _repository=new Repository<T>();

        public virtual T Find(Expression<Func<T, bool>> @where)
        {
          
           return _repository.Find(where);
        }

        public virtual List<T> List()
        {
            return _repository.List();
        }

        public virtual List<T> List(Expression<Func<T, bool>> @where)
        {
            return _repository.List(where);
        }

        public int Save()
        {
            return _repository.Save();
        }

        public virtual int Insert(T obj)
        {
            return _repository.Insert(obj);
        }

        public virtual int Delete(T obj)
        {
            return _repository.Delete(obj);
        }

        public virtual int Update(T obj)
        {
            return _repository.Update(obj);
        }

        public virtual IQueryable<T> ListQueryable()
        {
            return _repository.ListQueryable();
        }
    }
}
