using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogHulya.Models
{
    [Table("Notes")]
    public class Note:MyEntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string NoteImageName { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int StarPoint { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Category Category { get; set; }

        public virtual Member Member { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}