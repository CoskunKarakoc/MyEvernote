using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Common;
using MyEvernote.DataAccessLayer;
using MyEvernote.DataAccessLayer.EntityFramework.Abstract;
using MyEvernote.Entities;
using MyEvernote.Entities.Abstract;

namespace MyEvernote.DataAccessLayer.EntityFramework.Concrete
{
    public class Repository<T>: RepositoryBase,IRepository<T> where T: class,IEntity,new ()
    {
        private DbSet<T> _dbSet;
        public Repository()
        {
            _dbSet = context.Set<T>();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _dbSet.FirstOrDefault(where);
        }

        public List<T> List()
        {
           
            return _dbSet.ToList();
        }
        public IQueryable<T> ListQueryable()
        {
            return _dbSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public int Insert(T obj)
        {
            _dbSet.Add(obj);
            if (obj is MyEntityBase)
            {
                MyEntityBase o=obj as MyEntityBase;
                DateTime now=DateTime.Now;
                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUsername = App.Common.GetCurrentUsername(); 
            }
            return Save();
        }

        public int Delete(T obj)
        {
          
            //if (obj is MyEntityBase)
            //{
            //    MyEntityBase o = obj as MyEntityBase;
            //    o.ModifiedOn = DateTime.Now;
            //    o.ModifiedUsername = "System"; //TODO : burayada işlem yapan kullanıcı adı yazılmalı
            //}
            _dbSet.Remove(obj);
            return Save();
        }
        
        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;

                o.ModifiedOn = DateTime.Now;
                o.ModifiedUsername = App.Common.GetCurrentUsername();
            }

            return Save();
        }
     

    }
}
