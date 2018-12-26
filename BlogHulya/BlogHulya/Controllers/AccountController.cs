using BlogHulya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogHulya.Controllers
{
    
    [RoutePrefix("hesabim")]
    [Route("{action=Index}", Name = "Account")]
    public class AccountController : BaseController
    {

        // GET: Account
        [Route("uye-ol",Name = "Register")]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [Route("uye-ol")]
        [HttpPost]
        public ActionResult Register(Member user)
        {
            try
            {
                var mail = repo_member.Find(x => x.Email == user.Email);
                if (mail != null)
                {
                    throw new Exception("Zaten bu e-posta kayıtlıdır.");
                }
                user.MemberType = "customer";
                repo_member.Insert(user);
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                ViewBag.ReError = ex.Message;

                return View();
            }
            
        }
        [Route("giris",Name ="Login")]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [Route("giris")]
        [HttpPost]
        public ActionResult Login(Member model)
        {
            try
            {
                 var user = repo_member.Find(x => x.Password == model.Password && x.Email == model.Email);
                if (user != null)
                {
                    Session["LogonUser"] = user;
                   return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.reError = "Kullanıcı bilgileriniz yanlış";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.reError = ex.Message;
                return View();
            }
        }

        [Route("cikis",Name = "Logout")]
        public ActionResult Logout()
        {
            Session["LogonUser"] = null;
            return RedirectToAction("Login", "Account");
        }

        [Route("My-profile",Name = "Profil")]
        public ActionResult Profil(int id = 0)
        {
            if(id == 0)
            {
                id = base.CurrentUserId();
            }
           var user = repo_member.Find(x => x.Id == id);
            if (user == null) return RedirectToAction("Index", "Home");
            
            return View("Profil",user);
        }

        [Route("sifremi-unuttum",Name = "ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [Route("sifremi-unuttum")]
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            var member = repo_member.Find(x => x.Email == email);
            if (member == null)
            {
                ViewBag.MyError = "Böyle bir hesap bulunamadı.";
                return View();
            }
            else
            {
                var body = "Şifreniz : " + member.Password;
                MyMail mail = new MyMail(member.Email, "Şifremi Unuttum", body);
                mail.SendMail();
                TempData["Info"] = email + " mail adresinize şifreniz gönderilmiştir.";
                return RedirectToAction("Login");
            }
            
        }
    }
}