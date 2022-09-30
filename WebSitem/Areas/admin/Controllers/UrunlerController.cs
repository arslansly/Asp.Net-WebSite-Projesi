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
  
    public class UrunlerController : Controller
    {
        // GET: admin/Urunler
        public ActionResult Index()
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var model = db.urunler.ToList();
                return View(model);
            }

        }

        public ActionResult Yeni()
        {
            var model = new urunler();
            return View("UrunForm", model);
        }

        public ActionResult Guncelle(int ID)
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var model = db.urunler.Find(ID);
                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("UrunForm", model);
            }
        }
        public ActionResult Kaydet(urunler gelenUrun)
        {
            if(!ModelState.IsValid)
            {
                return View("UrunForm", gelenUrun);
            }
            using (DbKahveEntities db = new DbKahveEntities())
            {
                if (gelenUrun.ID == 0)     //yeni ürün kayıt
                {
                    if(gelenUrun.fotoFile==null)
                    {
                        ViewBag.HataFoto = "Lütfen Resim Yükleyin";
                        return View("UrunForm", gelenUrun);
                    }

                    string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);
                    gelenUrun.Foto = fotoAdi;
                    db.urunler.Add(gelenUrun);
                    gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), Path.GetFileName(fotoAdi)));
                    TempData["urun"] = "Urun basarili bir sekilde eklendi";
                }
                else                 //güncelleme
                {
                    var GuncellenecekVeri = db.urunler.Find(gelenUrun.ID);
                    if(gelenUrun.fotoFile!=null)
                    {
                        string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);
                        gelenUrun.Foto = fotoAdi;
                        gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/images"), Path.GetFileName(fotoAdi)));
                    }
                    db.Entry(GuncellenecekVeri).CurrentValues.SetValues(gelenUrun);
                    TempData["urun"] = "Urun basarili bir sekilde güncellendi";
                }
                db.SaveChanges();
                return RedirectToAction("index");
            }
            
            }


        public ActionResult Sil(int ID)
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var silinecekUrun = db.urunler.Find(ID);
                if(silinecekUrun==null)
                {
                    return HttpNotFound();
                }
                db.urunler.Remove(silinecekUrun);
                db.SaveChanges();
                TempData["urun"] = "Urun basarli bir sekilde silindi";
                return RedirectToAction("index");

            }
        }
        }
    }
