using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;

namespace MyEvernote.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Select(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryManager manager=new CategoryManager();
            Category category = manager.GetCategoryById(id.Value);
            if (category ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            TempData["Category"] = category.Notes.OrderByDescending(x=>x.ModifiedOn).ToList();
            return RedirectToAction("Index", "Home");
        }
    }
}