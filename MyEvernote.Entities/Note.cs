using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Core.Entities;

namespace MyEvernote.Entities
{
    [Table("Notes")]
   public class Note:MyEntityBase, IEntity
    {
        public Note()
        {
            Comments=new List<Comment>();
            Likes=new List<Liked>();
        }
        [Required,
         StringLength(60),
        DisplayName("Not Başlığı")]
        public string Title { get; set; }

        [Required,
         StringLength(2000),
        DisplayName("Not Metni")]
        public string Text { get; set; }
        [DisplayName("Taslak")]
        public bool isDraft { get; set; }
        [DisplayName("Beğenilme")]
        public int LikeCount { get; set; }
        [DisplayName("Kategori")]
        public int CategoryId { get; set; }

        public virtual EvernoteUser Owner { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Liked> Likes { get; set; }

    }
}
