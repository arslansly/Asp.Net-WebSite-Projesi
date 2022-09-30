using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSitem.App_Code;
using WebSitem.Models;


namespace WebSitem.Areas.admin.Controllers
{
   
    public class HakkimizdaController : Controller
    {
        // GET: admin/Hakkimizda
        public ActionResult Index()
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var model = db.hakkimizda.First();
                return View(model);
            }
                
        }

        public ActionResult HakkimizdaGüncelle()
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var model = db.hakkimizda.First();
                return View(model);
            }
        }

        public ActionResult Kaydet(hakkimizda GelenVeri)
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var GuncellenecekVeri = db.hakkimizda.First();
                if(!ModelState.IsValid)
                {
                    return View("HakkimizdaGüncelle", GelenVeri);
                }
                if (GelenVeri.fotoFile != null)
                {
                    GelenVeri.Foto = Seo.DosyaAdiDuzenle(GelenVeri.fotoFile.FileName);
                    GelenVeri.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), Path.GetFileName(GelenVeri.Foto)));

                }
                db.Entry(GuncellenecekVeri).CurrentValues.SetValues(GelenVeri);
                db.SaveChanges();
                TempData["hakkimdaGuncelle"]=" ";
                return RedirectToAction("index", "hakkimizda");
                
            }
        }
    }
}