using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        DbMvcOkulEntities2 db = new DbMvcOkulEntities2();
        public ActionResult Index()
        {
            var dersler = db.TBLDERSLER.ToList();
            return View(dersler);
        }
        [HttpGet]
        public ActionResult YeniDers()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDers(TBLDERSLER p)
        {
            db.TBLDERSLER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            try
            {
                var ders = db.TBLDERSLER.Find(id);
                db.TBLDERSLER.Remove(ders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            catch (Exception)
            {
                TempData["Hata"] = "Bu ders başka kayıtlarla ilişkili olduğundan silinemez";
            }

            return RedirectToAction("Index");
        }

        public ActionResult DersGetir(int id)
        {
            var ders = db.TBLDERSLER.Find(id);
            return View("DersGetir",ders);
        }

        public ActionResult Guncelle(TBLDERSLER d)
        {
            var ders = db.TBLDERSLER.Find(d.DERSID);
            ders.DERSAD = d.DERSAD;
            db.SaveChanges();
            return RedirectToAction("Index", "Default");
        }
    }
}