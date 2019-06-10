using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.BusinessLayer.Results;
using MyEvernote.Entities;
using MyEvernote.Entities.DataTransferObjects;
using MyEvernote.Entities.Messages;
using MyEvernote.Models;
using MyEvernote.ViewModels;


namespace MyEvernote.Controllers
{
    public class HomeController : Controller
    {
        NoteManager _noteManager = new NoteManager();
        EvernoteUserManager _evernoteUserManager = new EvernoteUserManager();
        CategoryManager _categoryManager = new CategoryManager();
        // GET: Home
        public ActionResult Index()
        {
            if (TempData["category"] != null)
            {
                return View(TempData["category"] as List<Note>);
            }


            //noteManager.GetAllNotesQueryable().OrderByDescending(x => x.ModifiedOn).ToList()
            return View(_noteManager.GetAllNotes().OrderByDescending(x => x.ModifiedOn));
        }


        public ActionResult MostLiked()
        {

            return View("Index", _noteManager.GetAllNotesQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult ShowProfile()
        {
            if (CurrentSession.User != null)
            {

                BusinessLayerResult<EvernoteUser> result = _evernoteUserManager.GetUserById(CurrentSession.User.Id);

                if (result.Errors.Count > 0)
                {
                    foreach (ErrorMessageObj error in result.Errors)
                    {
                        ErrorViewModel model = new ErrorViewModel()
                        {
                            Title = "Hata Oluştu",
                            RedirectingUrl = "/Home/Index",
                            List = result.Errors
                        };
                        return View("Error", model);
                    }
                }

                return View(result.Result);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditProfile()
        {
          
            if (CurrentSession.User != null)
            {

                BusinessLayerResult<EvernoteUser> result = _evernoteUserManager.GetUserById(CurrentSession.User.Id);
                if (result.Errors.Count > 0)
                {
                    foreach (ErrorMessageObj error in result.Errors)
                    {
                        ErrorViewModel errorViewModel = new ErrorViewModel()
                        {
                            Title = "Hata Oluştu",
                            RedirectingUrl = "/Home/Index",
                            List = result.Errors
                        };
                        return View("Error", errorViewModel);

                    }
                }

                return View(result.Result);

            }

            return View("Index");
        }
        [HttpPost]
        public ActionResult EditProfile(EvernoteUser user, HttpPostedFileBase ProfileImge)
        {
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                if (ProfileImge != null && (ProfileImge.ContentType == "image/jpeg" || ProfileImge.ContentType == "image/jpg" || ProfileImge.ContentType == "image/png"))
                {
                    string filname = $"user_{user.Id}.{ProfileImge.ContentType.Split('/')[1]}";
                    ProfileImge.SaveAs(Server.MapPath($"~/Images/{filname}"));
                    user.ProfileImageFileName = filname;
                }

                BusinessLayerResult<EvernoteUser> result = _evernoteUserManager.UpdateProfile(user);
                if (result.Errors.Count > 0)
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel()
                    {
                        List = result.Errors,
                        Title = "Profil Güncellenemedi",
                        RedirectingUrl = "/Home/EditProfile"
                    };
                    return View("Error", errorViewModel);
                }

               CurrentSession.Set<EvernoteUser>("login", result.Result);
                return RedirectToAction("ShowProfile");
            }

            return View(user);
        }

        public ActionResult RemoveProfile()
        {
          

            BusinessLayerResult<EvernoteUser> layerResult = _evernoteUserManager.RemoveUserById(CurrentSession.User.Id);
            if (layerResult.Errors.Count > 0)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel()
                {
                    List = layerResult.Errors,
                    Title = "Profil Silinemedi",
                    RedirectingUrl = "/Home/ShowProfile"
                };
                return View("Error", errorViewModel);

            }
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                BusinessLayerResult<EvernoteUser> user = _evernoteUserManager.LoginUser(model);
                if (user.Errors.Count > 0)
                {
                    //user.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    foreach (var err in user.Errors)
                    {
                        ModelState.AddModelError(err.Code.ToString(), err.Message);
                    }
                    return View(model);
                }

              CurrentSession.Set<EvernoteUser>("login",user.Result);//Session'a bilgi saklama
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }


        public ActionResult Test()
        {
            ErrorViewModel model = new ErrorViewModel()
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
            return View("Error", model);
        }



        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                BusinessLayerResult<EvernoteUser> result = _evernoteUserManager.RegisterUser(model);
                if (result.Errors.Count > 0)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                OkViewModel okViewModel = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Login"
                };
                okViewModel.List.Add("  Lütfen e-posta adresinize gönderdiğimiz aktivasyon linki'ne tıklayarak hesabınızı aktive ediniz.Hesabınızı aktive etmeden not ekleyemez ve beğenme yapamazsınız.");
                return View("Ok", okViewModel);
            }

            return View(model);
        }


        public ActionResult UserActivate(Guid id)
        {

            BusinessLayerResult<EvernoteUser> result = _evernoteUserManager.ActivateUser(id);

            if (result.Errors.Count > 0)
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    RedirectingUrl = "/Home/Index",
                    List = result.Errors
                };
                return View("Error", model);
            }

            OkViewModel okViewModel = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi",
                RedirectingUrl = "/Home/Login",

            };
            okViewModel.List.Add("Hesabınızla artık not paylaşabilir ve not beğenisi yapabilirsiniz.");
            return View("Ok", okViewModel);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }








        public ActionResult Select(int? id)
        {   /*Seçilen kategoriye göre listelemenin Tempdata kullanmadan yapılması*/
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = _categoryManager.Find(x => x.Id == id.Value);
            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }


            return View("Index", category.Notes.OrderByDescending(x => x.ModifiedOn));
        }
    }
}