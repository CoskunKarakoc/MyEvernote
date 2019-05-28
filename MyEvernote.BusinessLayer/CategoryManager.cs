using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.BusinessLayer.Abstarct;
using MyEvernote.DataAccessLayer.EntityFramework.Concrete;
using MyEvernote.Entities;

namespace MyEvernote.BusinessLayer
{
    public class CategoryManager:ManagerBase<Category>
    {
        
        #region İlişkiliCategorySilme
        /*Bu kısımda category silmeye çalıştığımızda ilişkili olduğu tablolarıda siliyoruz
       ama biz bunu veri tabanınında ilişki özelliklerini cascade yaparak otomatikleştirdik*/
        //public override int Delete(Category category)
        //{
        //    NoteManager noteManager=new NoteManager();
        //    LikedManager likedManager=new LikedManager();
        //    CommentManager commentManager=new CommentManager();
        //    foreach (Note note in category.Notes.ToList())
        //    {
        //        foreach (Liked liked in note.Likes.ToList())
        //        {
        //            likedManager.Delete(liked);
        //        }

        //        foreach (Comment comment in note.Comments.ToList())
        //        {
        //            commentManager.Delete(comment);
        //        }
        //        noteManager.Delete(note);
        //    }

        //    return base.Delete(category);
        //}
        #endregion
        
    }
}
