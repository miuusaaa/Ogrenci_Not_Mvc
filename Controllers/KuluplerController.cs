using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class KuluplerController : Controller
    {
        // GET: Kulupler
        DbMvcOkulEntities2 db = new DbMvcOkulEntities2();
        public ActionResult Index()
        { 
            var kulupler = db.TBLKULUPLER.ToList();
            return View(kulupler);
        }

        [HttpGet]
        public ActionResult YeniKulup()
        {   
            return View();
        }

        [HttpPost]
        public ActionResult YeniKulup(TBLKULUPLER p)
        {
            db.TBLKULUPLER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            try
            {
                var kulup = db.TBLKULUPLER.Find(id);
                db.TBLKULUPLER.Remove(kulup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            catch(Exception)
            {
                TempData["Hata"] = "Bu kulup baska kayıtlar ile iliskili oldugundan silinemez";
            }

            return RedirectToAction("Index");
        }

        public ActionResult KulupGetir(int id)
        {
            var kulup = db.TBLKULUPLER.Find(id);
            return View("KulupGetir",kulup);
        }

        public ActionResult Guncelle(TBLKULUPLER p)
        {
            var klp = db.TBLKULUPLER.Find(p.KULUPID);
            klp.KULUPAD = p.KULUPAD;
            klp.KULUPKONTENJAN = p.KULUPKONTENJAN;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}