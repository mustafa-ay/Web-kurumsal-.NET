using RitechWeb.Models.DataContext;
using RitechWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RitechWeb.Controllers
{
    public class AdminController : Controller
    {
        RitechWebDBContext db = new RitechWebDBContext();
        // GET: Admin
        [Route("AdminPanel")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("AdminPanel/giris")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult Login(Admin admin,string sifre)
        {
            var md5pass = Crypto.Hash(sifre, "MD5");
            var login = db.Admin.Where(x => x.Eposta == admin.Eposta).SingleOrDefault();
            if (login.Eposta==admin.Eposta&&login.Sifre==md5pass)
            {
                Session["adminid"] = login.AdminId;
                Session["eposta"] = login.Eposta;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.Uyari = "Kullanıcı Adı yada şifre yanlış";
            return View(admin);
        }
        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
            
        }
        public ActionResult Adminler()
        {
            return View(db.Admin.ToList());
        }
        public ActionResult Create()
        {
            return View();
           
        }
        [HttpPost]
        public ActionResult Create(Admin admin,string sifre,string eposta)
        {
            if (ModelState.IsValid)
            {
                admin.Sifre = Crypto.Hash(sifre, "MD5");
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View(admin);

        }
        public ActionResult Edit(int id)
        {
            var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
            return View(a);

        }
        [HttpPost]
        public ActionResult Edit(int id,Admin admin,string sifre,string eposta)
        {
            if (ModelState.IsValid)
            {
                var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
                a.Sifre = Crypto.Hash(sifre, "MD5");
                a.Eposta =admin.Eposta;
                a.Yetki = admin.Yetki;
                db.SaveChanges();
                return RedirectToAction("Adminler");
            }            
            return View(admin);
        }
        public ActionResult Delete(int id)
        {
            var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
            if (a!=null)
            {
                db.Admin.Remove(a);
                db.SaveChanges();
                return RedirectToAction("Adminler");
                    
            }
            return View();
        }
    }
}