using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSitem.App_Code;
using WebSitem.Models;
using System.IO;

namespace WebSitem.Areas.admin.Controllers
{
    public class MagazaController : Controller
    {
        // GET: admin/Magaza
        public ActionResult Index()
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var model = db.magaza.ToList();
                return View(model);
            }

        }

        public ActionResult Yeni()
        {
            var model = new magaza();
            return View("MagazaForm", model);
        }

        public ActionResult Guncelle(int ID)
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var model = db.magaza.Find(ID);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("MagazaForm", model);
            }
        }
        public ActionResult Kaydet(magaza gelenMagaza)
        {
            if (!ModelState.IsValid)
            {
                return View("MagazaForm", gelenMagaza);
            }
            using (DbKahveEntities db = new DbKahveEntities())
            {
                if (gelenMagaza.ID == 0)     //yeni ürün kayıt
                {
                    if (gelenMagaza.fotoFile == null)
                    {
                        ViewBag.HataFoto = "Lütfen Resim Yükleyin";
                        return View("MagazaForm", gelenMagaza);
                    }

                    string fotoAdi = Seo.DosyaAdiDuzenle(gelenMagaza.fotoFile.FileName);
                    gelenMagaza.Foto = fotoAdi;
                    db.magaza.Add(gelenMagaza);
                    gelenMagaza.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), Path.GetFileName(fotoAdi)));
                    TempData["magaza"] = "Urun basarili bir sekilde eklendi";
                }
                else                 //güncelleme
                {
                    var GuncellenecekVeri = db.magaza.Find(gelenMagaza.ID);
                    if (gelenMagaza.fotoFile != null)
                    {
                        string fotoAdi = Seo.DosyaAdiDuzenle(gelenMagaza.fotoFile.FileName);
                        gelenMagaza.Foto = fotoAdi;
                        gelenMagaza.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), Path.GetFileName(fotoAdi)));
                    }
                    db.Entry(GuncellenecekVeri).CurrentValues.SetValues(gelenMagaza);
                    TempData["magaza"] = "Urun basarili bir sekilde güncellendi";
                }
                db.SaveChanges();
                return RedirectToAction("index");
            }

        }


        public ActionResult Sil(int ID)
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var silinecekMagaza = db.magaza.Find(ID);
                if (silinecekMagaza == null)
                {
                    return HttpNotFound();
                }
                db.magaza.Remove(silinecekMagaza);
                db.SaveChanges();
                TempData["magaza"] = "Urun basarli bir sekilde silindi";
                return RedirectToAction("index");

            }
        }
    }
}