using RitechWeb.Models.DataContext;
using RitechWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RitechWeb.Controllers
{
    public class PortfolyoController : Controller
    {
        private RitechWebDBContext db = new RitechWebDBContext();
        // GET: Portfolyo
        public ActionResult Index()
        {
            return View(db.Portfolyo.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Portfolyo portfolyo,HttpPostedFileBase LogoURL)
        {
            if (ModelState.IsValid)
            {
                if (LogoURL != null)
                {
                    
                    WebImage img = new WebImage(LogoURL.InputStream);
                    FileInfo imginfo = new FileInfo(LogoURL.FileName);

                    string logoname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/Portfolyo/" + logoname);

                    portfolyo.LogoURL = "/Uploads/Portfolyo/" + logoname;
                }
                db.Portfolyo.Add(portfolyo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(portfolyo);
        }
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                ViewBag.Uyari = "Güncellenecek Portfolyo bulunamadı";
            }
            var portfolyo = db.Portfolyo.Find(id);
            if (portfolyo==null)
            {
                return HttpNotFound();
            }
            return View(portfolyo);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id,Portfolyo portfolyo,HttpPostedFileBase LogoURL)
        {
            
            if (ModelState.IsValid)
            {
                var p = db.Portfolyo.Where(x => x.PortfolyoId == id).SingleOrDefault();
                if (LogoURL!=null)
                {
                    
                        if (System.IO.File.Exists(Server.MapPath(p.LogoURL)))
                        {
                            System.IO.File.Delete(Server.MapPath(p.LogoURL));
                        }
                        WebImage img = new WebImage(LogoURL.InputStream);
                        FileInfo imginfo = new FileInfo(LogoURL.FileName);

                        string portfolyoname = Guid.NewGuid().ToString() + imginfo.Extension;
                        img.Resize(500, 500);
                        img.Save("~/Uploads/Portfolyo/" + portfolyoname);

                        p.LogoURL = "/Uploads/Portfolyo/" + portfolyoname;
                    
                }

                p.Baslik = portfolyo.Baslik;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            if (id==null)
            {
                return HttpNotFound();
            }
            var p = db.Portfolyo.Find(id);
            if (p==null)
            {
                return HttpNotFound();
            }
            db.Portfolyo.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");
            

        }
    }
}