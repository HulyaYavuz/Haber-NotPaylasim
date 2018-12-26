using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogHulya.Models
{
    [Table("Comments")]
    public class Comment:MyEntityBase
    {
        public string Text { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsChecked { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Member Member { get; set; }

        public virtual Note Note { get; set; }


    }
}