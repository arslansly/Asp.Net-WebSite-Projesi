using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSitem.Models;


namespace WebSitem.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController()
        {

        }
        // GET: Home
        //[Route("Anasayfa")]
        public ActionResult Index()
        {
            var list= new List<xxx>

            using (DbKahveEntities db = new DbKahveEntities())
            {
                var model = db.anasayfa.Find(1);
                return View(model);
            }
        }
        [Route("Hakkimizda")]
        public ActionResult About()
        {
            using (DbKahveEntities db=new DbKahveEntities())
            {
                var model = db.hakkimizda.Find(1);
                return View(model);
            }      
        }
        [Route("Urunler")]
        public ActionResult Products()
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var model = db.urunler.ToList();
                return View(model);
            }             
        }
        [Route("Magaza")]
        public ActionResult Store()
        {
            using (DbKahveEntities db = new DbKahveEntities())
            {
                var model = db.magaza.ToList();
                return View(model);
            }
        }
        [Route("Iletisim")]
        public ActionResult Iletisim()
        {
            return View();
        }
    }
}

