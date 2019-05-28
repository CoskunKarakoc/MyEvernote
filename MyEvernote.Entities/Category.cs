using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Core.Entities;

namespace MyEvernote.Entities
{
    [Table("Categories")]
    public class Category:MyEntityBase,IEntity
    {
        public Category()
        {
            Notes=new List<Note>();
        }
        [DisplayName("Başlık"),
         Required(ErrorMessage = "{0} alanının girilmesi zorunludur."),
         StringLength(50,ErrorMessage = "{0} alanının max. {1} karakterden oluşturulması gerekiyor.")]
        public string Title { get; set; }
        [DisplayName("Açıklama"),
         StringLength(200,ErrorMessage = "{0} alanının max. {1} karakterden oluşturulması gerekiyor.")]
        public string Description { get; set; }

        public virtual List<Note> Notes { get; set; }   

    }
}
