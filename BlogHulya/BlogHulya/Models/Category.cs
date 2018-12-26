using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogHulya.Models
{
    [Table("Categories")]
    public class Category:MyEntityBase
    {
        public string Title { get; set; }

        public virtual List<Note> Notes { get; set; }
    }
}