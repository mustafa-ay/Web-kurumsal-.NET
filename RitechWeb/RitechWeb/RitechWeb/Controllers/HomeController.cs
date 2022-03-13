using RitechWeb.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RitechWeb.Controllers
{
    public class HomeController : Controller
    {
        private RitechWebDBContext db = new RitechWebDBContext();
        // GET: Home
        [Route("")]
        [Route("Anasayfa")]
        public ActionResult Index()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Hizmet = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);


            return View();
        }

        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList().OrderByDescending(x=>x.SliderId));
        }
        public ActionResult HizmetPartial()
        {
            return View(db.Hizmet.ToList());
        }
        [Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {
            
        ViewBag.Kimlik = db.Kimlik.SingleOrDefault();

            return View(db.Hakkimizda.SingleOrDefault());
        }
        [Route("Hizmet")]
        public ActionResult Hizmet()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hizmet.ToList().OrderByDescending(x=>x.HizmetId));
        }
        [Route("Portfolyo")]
        public ActionResult Portfolyo()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Portfolyo.ToList().OrderByDescending(x => x.PortfolyoId));
        }
        [Route("Iletisim")]
        public ActionResult Iletisim()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Iletisim.SingleOrDefault());
        }
        [HttpPost]
        public ActionResult Iletisim(string adsoyad=null, string email=null, string konu=null, string mesaj=null)
        {
            if (adsoyad!=null && email!=null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "ritech@ritech.com";
                WebMail.Password = "admin123";
                WebMail.SmtpPort = 25;
                WebMail.Send("info@ritech.com.tr",konu,email+"-"+ mesaj);
                ViewBag.Uyari = "Mesajınız başarı ile gönderilmiştir.";
            }
            else
            {
                ViewBag.Uyari = "Hata oluştu tekrar deneyiniz";
            }
            return View();
        }

        public ActionResult FooterPartial()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Hizmet = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);

            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
           
            return PartialView();
        }
       
    }
}