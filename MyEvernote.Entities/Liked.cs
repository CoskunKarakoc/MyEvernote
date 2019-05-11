using MyEvernote.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Likes")]
    public class Liked:IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  Id { get; set; } 

        public virtual Note Note { get; set; }

        public virtual EvernoteUser LikedUser { get; set; }  

    }
}
