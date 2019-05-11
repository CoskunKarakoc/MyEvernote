using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccessLayer.EntityFramework.Concrete;
using MyEvernote.Entities;

namespace MyEvernote.BusinessLayer
{
    public class CategoryManager:Repository<Category>
    {
       

        public List<Category> GetCategories()
        {
            return List();
        }


        public Category GetCategoryById(int id)
        {
            return Find(x => x.Id == id);
        }

    }
}
