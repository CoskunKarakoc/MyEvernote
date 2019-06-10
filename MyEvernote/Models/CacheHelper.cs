using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;

namespace MyEvernote.Models
{
    public class CacheHelper
    {
        public static List<Category> GetCategoryFromCache()
        {
            CategoryManager categoryManager=new CategoryManager();
            var result = WebCache.Get("category-cache");
            if (result==null)
            {
                result = categoryManager.List();
                WebCache.Set("category-cache",result,20,true);
            }

            return result;
        }

        public static void RemoveCategoriesFromCache()
        {
            Remove("category-cache");
        }

        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }
    }
}