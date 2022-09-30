using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSitem.Models;
using WebSitem.App_Code;
using System.IO;

namespace WebSitem.Areas.admin.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: admin/Anasayfa
        public ActionResult Index()
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var model = db.anasayfa.First();
                return View(model);
            }

        }

        public ActionResult AnasayfaGüncelle()
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var model = db.anasayfa.First();
                return View(model);
            }
        }

        public ActionResult Kaydet(anasayfa GelenVeriAnasayfa)
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var GuncellenecekVeriAnasayfa = db.anasayfa.First();
                if (!ModelState.IsValid)
                {
                    return View("AnasayfaGüncelle", GelenVeriAnasayfa);
                }
                if (GelenVeriAnasayfa.fotoFile != null)
                {
                    GelenVeriAnasayfa.Foto = Seo.DosyaAdiDuzenle(GelenVeriAnasayfa.fotoFile.FileName);
                    GelenVeriAnasayfa.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), Path.GetFileName(GelenVeriAnasayfa.Foto)));

                }
                db.Entry(GuncellenecekVeriAnasayfa).CurrentValues.SetValues(GelenVeriAnasayfa);
                db.SaveChanges();
                TempData["anasayfaGuncelle"] = " ";
                return RedirectToAction("index", "anasayfa");

            }
        }
    }
}