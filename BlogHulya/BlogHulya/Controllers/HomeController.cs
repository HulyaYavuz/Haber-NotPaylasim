using BlogHulya.Filter;
using BlogHulya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogHulya.Controllers
{

    [Route("{action=Index}",Name ="Home")]
    public class HomeController : BaseController
    {
        List<Note> note = new List<Note>();
        List<Category> category = new List<Category>();
        // GET: Home
        [Route("~/haberler/tum-haberler",Name = "Index")]
        public ActionResult Index()
        {
            note = repo_note.List().OrderByDescending(x => x.AddedDate).Where(x => x.IsDeleted == false).ToList();
            return View(note);
        }

        [Route("haberler/{category}/{newsTitle}-{id:int}")]
        [HttpGet]
        public ActionResult GetNoteDetail(int? id)
        {
            var note = repo_note.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            TempData["detailnote"] = note.Id;
            return View("GetNoteDetail", note);
        }
        [Route("haberler/{category}/{newsTitle}-{id:int}")]
        [HttpPost]
        public ActionResult GetNoteDetail(Comment comment,string text)
        {
            var currentuser = Session["LogonUser"];
            var NoteId = TempData["detailnote"];
            
            List<Note> not = repo_note.List();
            Note note = not.Find(x => x.Id == (int)NoteId);

            comment.Member = base.CurentUser();
            comment.Note = note;
            comment.Text = text;
            comment.AddedDate = DateTime.Now;
            repo_comment.Insert(comment);

            return RedirectToAction("GetNoteDetail","Home");
        }
    }
}