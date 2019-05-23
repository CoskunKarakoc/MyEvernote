﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Core.Entities;

namespace MyEvernote.Entities
{
    [Table("Comments")]
    public class Comment:MyEntityBase, IEntity
    {
        [Required,StringLength(300)]
        public string Text { get; set; }
        public virtual Note Note { get; set; }
        public virtual EvernoteUser Owner { get; set; }

    }
}
