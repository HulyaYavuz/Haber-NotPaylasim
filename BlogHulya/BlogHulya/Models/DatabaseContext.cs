using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogHulya.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}