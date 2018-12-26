using BlogHulya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogHulya.Controllers
{
    public class BaseController : Controller
    {
        public Repository<Member> repo_member = new Repository<Member>();
        public Repository<Note> repo_note = new Repository<Note>();
        public Repository<Comment> repo_comment = new Repository<Comment>();
        public Repository<Category> repo_category = new Repository<Category>();
        // GET: Base
        public BaseController()
        {

        }
        protected Member CurentUser()
        {
            return (Member)Session["LogonUser"];
        }
        protected int CurrentUserId()
        {
            return ((Member)Session["LogonUser"]).Id;
        }
        protected bool IsLogon()
        {
            if (Session["LogonUser"] == null)
                return false;
            else
                return true;
        }
    }
}