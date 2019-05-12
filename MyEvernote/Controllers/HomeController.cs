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
using MyEvernote.Entities.Messages;
using MyEvernote.ViewModels;


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
            
            //noteManager.GetAllNotesQueryable().OrderByDescending(x => x.ModifiedOn).ToList()
            return View(noteManager.GetAllNotes().OrderByDescending(x => x.ModifiedOn));
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


        public ActionResult ShowProfile()
        {
           EvernoteUser user=Session["login"] as EvernoteUser;
           EvernoteUserManager manager=new EvernoteUserManager();
           BusinessLayerResult<EvernoteUser> result = manager.GetUserById(user.Id);

           if (result.Errors.Count>0)
           {
               foreach (ErrorMessageObj error in result.Errors)
               {
                  //TODO: Hata olduğunda yönlenecek kısım
               }
           }
           
            return View(result.Result);
        }

        public ActionResult EditProfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditProfile(EvernoteUser user)
        {
            return View();
        }

        public ActionResult RemoveProfile()
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
                BusinessLayerResult<EvernoteUser> user = manager.LoginUser(model);
                if (user.Errors.Count > 0)
                {
                    //user.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    foreach (var err in user.Errors)
                    {
                        ModelState.AddModelError(err.Code.ToString(),err.Message);
                    }
                    return View(model);
                }

                Session["login"] = user.Result;
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }


        public ActionResult Test()
        {
            ErrorViewModel model=new ErrorViewModel()
            {
                
                Title = "uyarı",
                Header = "İşlem uyarılı",
                IsRedirecting = true,
                RedirectingTimeOut = 8000,
                RedirectingUrl = "/Home/Index",
                List = new List<ErrorMessageObj>()
                {
                    new ErrorMessageObj(){Message = "message 1"},
                    new ErrorMessageObj(){Message = "message 2"},
                    new ErrorMessageObj(){Message = "message 3"},
                    new ErrorMessageObj(){Message = "message 4"}
                    }
            };
            return View("Error",model);
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

        public ActionResult UserActivate(Guid id)
        {
            EvernoteUserManager manager = new EvernoteUserManager();
            BusinessLayerResult<EvernoteUser> result = manager.ActivateUser(id);

            if (result.Errors.Count>0)
            {
                TempData["errors"] = result.Errors;
                return RedirectToAction("UserActivateCancel");
            }
            return RedirectToAction("UserActivateOk");
        }
        public ActionResult UserActivateOk()
        {
            return View();
        }
        public ActionResult UserActivateCancel()
        {
            List<ErrorMessageObj> errors=null;
            if (TempData["errors"]!=null)
            {
                errors = TempData["errors"] as List<ErrorMessageObj>; 
            }
            return View(errors);
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