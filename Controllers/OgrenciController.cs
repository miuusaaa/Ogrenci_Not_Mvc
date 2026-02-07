using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class OgrenciController : Controller
    {
        // GET: Ogrenci
        DbMvcOkulEntities2 db = new DbMvcOkulEntities2();
        public ActionResult Index()
        {
            var ogrenciler = db.TBLOGRENCILER.ToList();
            return View(ogrenciler);
        }

        [HttpGet]
        public ActionResult YeniOgrenci()
        {
            //List<SelectListItem> items = new List<SelectListItem>();

            //items.Add(new SelectListItem { Text = "Seçiniz", Value = "0" , Selected = true});

            //items.Add(new SelectListItem { Text = "Kürtçe", Value = "1" });

            //items.Add(new SelectListItem { Text = "Almanca", Value = "2" });

            //items.Add(new SelectListItem { Text = "Beden", Value = "3" });

            //items.Add(new SelectListItem { Text = "İngilizce", Value = "4" });

            //ViewBag.DersAd = items;

            //AŞAĞIDA DROPDOWNLIST İLE KULUPLERİ VERİTABANINDAN ÇEKME KODU MEVCUT.

            List<SelectListItem> degerler = (from i in db.TBLKULUPLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KULUPAD,
                                                 Value = i.KULUPID.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniOgrenci(TBLOGRENCILER p3)
        {
            var klp = db.TBLKULUPLER.Where(m => m.KULUPID == p3.TBLKULUPLER.KULUPID).FirstOrDefault();
            p3.TBLKULUPLER = klp;
            db.TBLOGRENCILER.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            try
            {
                var ogrenci = db.TBLOGRENCILER.Find(id);
                db.TBLOGRENCILER.Remove(ogrenci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            catch (Exception)
            {
                TempData["Hata"] = "Bu öğrenci başka kayıtlarda kullanıldığı için silinemez!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult OgrenciGetir(int id)
        {
            var ogrenci = db.TBLOGRENCILER.Find(id);


            List<SelectListItem> degerler = (from i in db.TBLKULUPLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KULUPAD,
                                                 Value = i.KULUPID.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;

            return View("OgrenciGetir", ogrenci);
        }

        public ActionResult Guncelle(TBLOGRENCILER o)
        {
            var ogr = db.TBLOGRENCILER.Find(o.OGRENCIID);
            ogr.OGRFOTO = o.OGRFOTO;
            ogr.OGRSOYAD = o.OGRSOYAD;
            ogr.OGRAD = o.OGRAD;
            ogr.OGRKULUP = o.OGRKULUP;
            ogr.OGRCINSIYET = o.OGRCINSIYET;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}