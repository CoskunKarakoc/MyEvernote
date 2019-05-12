﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entities.Abstract;

namespace MyEvernote.Entities
{
    [Table("Categories")]
    public class Category:MyEntityBase,IEntity
    {
        public Category()
        {
            Notes=new List<Note>();
        }
        [Required,StringLength(50)]
        public string Title { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        public virtual List<Note> Notes { get; set; }   

    }
}