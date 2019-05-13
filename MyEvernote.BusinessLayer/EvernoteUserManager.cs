using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Common.Helpers;
using MyEvernote.DataAccessLayer.EntityFramework.Concrete;
using MyEvernote.Entities;
using MyEvernote.Entities.DataTransferObjects;
using MyEvernote.Entities.Messages;

namespace MyEvernote.BusinessLayer
{
    public class EvernoteUserManager : Repository<EvernoteUser>
    {

        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data)
        {
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();



            EvernoteUser user = Find(x => x.UserName == data.Username || x.Email == data.EMail);
            if (user != null)
            {
                if (user.UserName == data.Username)
                {
                    layerResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı");
                }

                if (user.Email == data.EMail)
                {
                    layerResult.AddError(ErrorMessageCode.EMailAlreadyExists, "E-Mail hesabı daha önceden kullanılıyor.");
                }
            }
            else
            {
                int dbResult = Insert(new EvernoteUser()
                {
                    UserName = data.Username,
                    Email = data.EMail,
                    Password = data.Password,
                    ProfileImageFileName = "user.png",
                    ActivateGuid = Guid.NewGuid(),
                    isActive = false,
                    isAdmin = false,
                });
                if (dbResult > 0)
                {
                    layerResult.Result = Find(x => x.UserName == data.Username && x.Email == data.EMail);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}Home/UserActivate/{layerResult.Result.ActivateGuid}";
                    string body = $"<h1>Merhaba <strong>{layerResult.Result.UserName}</strong></h1><br/><br/><h4>Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız.</a></h4>";
                    MailHelper.SendMail(body, layerResult.Result.Email, "MyEvernote Hesap Aktifleştirme");

                }
            }

            return layerResult;



        }

        public BusinessLayerResult<EvernoteUser> GetUserById(int id)
        {
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
            layerResult.Result = Find(x => x.Id == id);
            if (layerResult.Result == null)
            {
                layerResult.AddError(ErrorMessageCode.UserNotFound, "Böyle bir kullanıcı bulunamadı");
            }


            return layerResult;
        }

        public BusinessLayerResult<EvernoteUser> LoginUser(LoginViewModel data)
        {

            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
            layerResult.Result = Find(x => x.UserName == data.Username && x.Password == data.Password);
            if (layerResult.Result != null)
            {
                if (!layerResult.Result.isActive)
                {
                    layerResult.AddError(ErrorMessageCode.UserIsNotActive, "Kulllanıcı aktifleştirilmemiştir. ");
                    layerResult.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen e-posta adresinizi kontrol ediniz.");

                }
            }
            else
            {
                layerResult.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı Adı yada Şifre Uyuşmuyor!");

            }

            return layerResult;
        }

        public BusinessLayerResult<EvernoteUser> ActivateUser(Guid activate_id)
        {
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
            layerResult.Result = Find(x => x.ActivateGuid == activate_id);
            if (layerResult.Result != null)
            {
                if (layerResult.Result.isActive)
                {
                    layerResult.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return layerResult;
                }

                layerResult.Result.isActive = true;
                Update(layerResult.Result);
            }
            else
            {
                layerResult.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı.");

            }

            return layerResult;
        }

        public BusinessLayerResult<EvernoteUser> RemoveUserById(int id)
        {
          BusinessLayerResult<EvernoteUser> layerResult=new BusinessLayerResult<EvernoteUser>();
          EvernoteUser user = Find(x => x.Id == id);
          if (user!=null)
          {
              if (Delete(user)==0)
              {
                  layerResult.AddError(ErrorMessageCode.ActivateIdDoesNotExists,"Kullanıcı Silinemedi.");
                  return layerResult;
              }
          }
          else
          {
              layerResult.AddError(ErrorMessageCode.ActivateIdDoesNotExists,"Kullanıcı Bulunamadı.");
          }
          return layerResult;
        }

        public BusinessLayerResult<EvernoteUser> UpdateProfile(EvernoteUser data)
        {
            EvernoteUser db_user = Find(x => x.UserName == data.UserName || x.Email == data.Email);
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
            layerResult.Result = data;

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.UserName == data.UserName)
                {
                    layerResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    layerResult.AddError(ErrorMessageCode.EMailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return layerResult;
            }

            layerResult.Result = Find(x => x.Id == data.Id);
            layerResult.Result.Email = data.Email;
            layerResult.Result.Name = data.Name;
            layerResult.Result.Surname = data.Surname;
            layerResult.Result.Password = data.Password;
            layerResult.Result.UserName = data.UserName;
           

            if (!string.IsNullOrEmpty(data.ProfileImageFileName))
            {
                layerResult.Result.ProfileImageFileName = data.ProfileImageFileName;
            }

            if (Update(layerResult.Result) == 0)
            {
                layerResult.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Kullanıcı güncellenemedi.");
            }

            return layerResult;

        }
    }
}
