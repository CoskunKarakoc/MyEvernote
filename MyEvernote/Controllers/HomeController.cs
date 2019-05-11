using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Entities.DataTransferObjects;


namespace MyEvernote.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            if (TempData["category"] != null)
            {
                return View(TempData["category"] as List<Note>);
            }
            NoteManager noteManager = new NoteManager();
            //noteManager.GetAllNotes().OrderByDescending(x => x.ModifiedOn);
            return View(noteManager.GetAllNotesQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
        }


        public ActionResult MostLiked()
        {
            NoteManager manager = new NoteManager();
            return View("Index", manager.GetAllNotesQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult Login()
        {




            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            EvernoteUserManager manager = new EvernoteUserManager();
            if (ModelState.IsValid)
            {
                BusinessLayerResult<EvernoteUser> user= manager.LoginUser(model);
                if (user.Errors.Count>0)
                {
                    user.Errors.ForEach(x=>ModelState.AddModelError("",x.Message));
                    return View(model);
                }

                Session["login"] = user.Result;
                return RedirectToAction("Index","Home");
            }

            return View(model);
        }
            

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            EvernoteUserManager manager = new EvernoteUserManager();
            BusinessLayerResult<EvernoteUser> result = manager.RegisterUser(model);
            if (ModelState.IsValid)
            {
                if (result.Errors.Count > 0)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                return RedirectToAction("RegisterOk", "Home");
            }

            return View(model);
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult UserActivate(Guid activateId)
        {
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }








        //public ActionResult Select(int? id)
        //{   /*Seçilen kategoriye göre listelemenin Tempdata kullanmadan yapılması*/
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CategoryManager manager = new CategoryManager();
        //    Category category = manager.GetCategoryById(id.Value);
        //    if (category == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        //    }


        //    return View("Index", category.Notes.OrderByDescending(x=>x.ModifiedOn));
        //}
    }
}