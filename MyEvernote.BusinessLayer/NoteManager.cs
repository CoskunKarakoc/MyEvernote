using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccessLayer.EntityFramework.Concrete;
using MyEvernote.Entities;

namespace MyEvernote.BusinessLayer
{
    public class NoteManager:Repository<Note>
    {
        public List<Note> GetAllNotes()
        {
             return List();
        }
        public IQueryable<Note> GetAllNotesQueryable()
        {
            return ListQueryable();
        }
    }
}
