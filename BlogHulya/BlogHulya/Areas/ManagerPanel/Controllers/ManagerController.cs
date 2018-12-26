using BlogHulya.Controllers;
using BlogHulya.Filter;
using BlogHulya.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogHulya.Areas.ManagerPanel.Controllers
{
   // [RouteArea("Yonetim-Paneli")]
    [ValidateInput(false)]
    [MyAuthorization(_memberType: "Admin")]
    public class ManagerController : BaseController
    {
        // GET: ManagerPanel/Manager
        
        public ActionResult Index()
        {
            List<Note> note = new List<Note>();
            note = repo_note.List().OrderByDescending(x => x.AddedDate).Where(x => x.IsDeleted == false).ToList();
            return View(note);
        }

        
        public ActionResult AddToNote()
        {
            var notes = repo_note.List().Where(x => x.IsDeleted == false);
            return View(notes.OrderByDescending(x => x.AddedDate).ToList());
        }


        public ActionResult Edit(int id = 0)
        {
            var note = repo_note.Find(x => x.Id == id);
            return View(note);
        }

        [HttpPost]
        public ActionResult Edit(Note note)
        {
            var noteImagePath = string.Empty;

            if (Request.Files != null && Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    var folder = Server.MapPath("~/Image/NoteImage");
                    var filename = Guid.NewGuid() + ".jpg";
                    file.SaveAs(Path.Combine(folder, filename));

                    var filePath = "Image/NoteImage/" + filename;
                    noteImagePath = filePath;
                }
            }

            if (note.Id > 0)
            {
                var dbNote = repo_note.Find(x => x.Id == note.Id);
                dbNote.Category.Title = note.Category.Title;
                dbNote.ModifiedDate = DateTime.Now;
                dbNote.Description = note.Description;
                dbNote.Title = note.Title;
                dbNote.IsDeleted = false;
                if (string.IsNullOrEmpty(noteImagePath) == false)
                {
                    dbNote.NoteImageName = noteImagePath;
                }

            }
            else
            {
                note.AddedDate = DateTime.Now;
                note.ModifiedDate = DateTime.Now;
                note.IsDeleted = false;
                note.Category.Id = 1;
                note.Member = CurentUser();
                note.Category.Title = note.Category.Title;
                note.NoteImageName = noteImagePath;
                repo_note.Insert(note);
            }
            repo_note.Save();
            return RedirectToAction("AddToNote");
        }

        public ActionResult Delete(int id)
        {
            var not = repo_note.Find(x => x.Id == id);
            not.IsDeleted = true;
            repo_note.Save();
            return RedirectToAction("AddToNote");
        }

        public ActionResult ConfirmComment()
        {
            var comments = repo_comment.List();

            return View(comments);
        }

        public ActionResult Confirm(int id)
        {
            var comment = repo_comment.Find(x => x.Id == id);
            comment.IsChecked = true;
            repo_comment.Save();

            return RedirectToAction("ConfirmComment");
        }

        public ActionResult AddToRole()
        {
            var members = repo_member.List();
            return View(members.OrderByDescending(x => x.MemberType).ToList());
        }
        
        public ActionResult EditRole(int id = 0)
        {
            var member = repo_member.Find(x => x.Id == id);
            return View(member);
        }
        [HttpPost]
        public ActionResult EditRole(Member member)
        {
            if (member.Id > 0)
            {
                var dbMember = repo_member.Find(x => x.Id == member.Id);
                dbMember.MemberType = member.MemberType;
               
            }
            repo_member.Save();
            return RedirectToAction("AddToRole");

        }
      
    }
}