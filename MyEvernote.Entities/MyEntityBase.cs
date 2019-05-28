using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    public class MyEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Oluşturma Tarihi"),
         Required(ErrorMessage = "{0} alanının girilmesi zorunludur.")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Güncelleme Tarihi"),
         Required(ErrorMessage = "{0} alanının girilmesi zorunludur.")]
        public DateTime ModifiedOn { get; set; }
        [DisplayName("Güncelleyen Kullanıcı"),
         Required(ErrorMessage = "{0} alanının girilmesi zorunludur."),
         StringLength(30,ErrorMessage = "{0} alanının max. {1} karakterden oluşturulması gerekiyor.")]
        public string ModifiedUsername { get; set; }
    }
}
