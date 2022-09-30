using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSitem.Models;
using System.IO;



namespace WebSitem.Areas.admin.Controllers
{
    public class GirisController : Controller
    {
        // GET: admin/Giris
        [HttpGet]
        public ActionResult Index()
        {
            return View("Giris");
        }
        [HttpPost]
        public ActionResult Kontrol(WebSitem.Models.kullanici Kullanicilar)
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var bilgiler = db.kullanici.Where(x => x.kAd == Kullanicilar.kAd && x.Sifre == Kullanicilar.Sifre).FirstOrDefault();
                if (bilgiler != null) {
                    Session["userID"] = bilgiler.ID;
                    Session["kullanici"] = bilgiler.kAd;
                    return RedirectToAction("Index","Anasayfa");
                }
            }
            return View("Giris");
        }
        [HttpGet]
        public ActionResult Logout() 
        {
            Session.Abandon();
            return RedirectToAction("Index", "Giris");
        }
    }
}