using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogHulya.Models
{
    [Table("Members")]
    public class Member : MyEntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilImageName { get; set; }
        public string MemberType { get; set; }
        public int Time { get; set; }

        public virtual List<Note> Notes { get; set; }

        public virtual List<Comment> Comments { get; set; }


    }
}