using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models;
using OgrenciNotMvc.Models.EntityFramework;
using System.Data.Entity;

namespace OgrenciNotMvc.Controllers
{
    public class NotlarController : Controller
    {
        // GET: Notlar
        DbMvcOkulEntities2 db = new DbMvcOkulEntities2();
        public ActionResult Index()
        {
            var SinavNotlar = db.TBLNOTLAR
        .Include(x => x.TBLOGRENCILER)
        .Include(x => x.TBLDERSLER)
        .ToList();

            return View(SinavNotlar);
        }
        [HttpGet]
        public ActionResult YeniSinav()
        {
            List<SelectListItem> dersler = (from i in db.TBLDERSLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.DERSAD,
                                                 Value = i.DERSID.ToString()
                                             }).ToList();

            ViewBag.drs = dersler;

            List<SelectListItem> ogrenciler = (from i in db.TBLOGRENCILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.OGRAD + " " + i.OGRSOYAD,
                                                 Value = i.OGRENCIID.ToString()
                                             }).ToList();

            ViewBag.ogr = ogrenciler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniSinav(TBLNOTLAR not)
        {
            var ogrenciler = db.TBLOGRENCILER.Where(m => m.OGRENCIID == not.TBLOGRENCILER.OGRENCIID).FirstOrDefault();
            var dersler = db.TBLDERSLER.Where(m => m.DERSID == not.TBLDERSLER.DERSID).FirstOrDefault();
            not.TBLOGRENCILER= ogrenciler;
            not.TBLDERSLER = dersler;
            db.TBLNOTLAR.Add(not);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult NotGetir(int id)
        {
            List<SelectListItem> ogrenciler = (from i in db.TBLOGRENCILER.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.OGRAD + " " + i.OGRSOYAD,
                                                   Value = i.OGRENCIID.ToString()
                                               }).ToList();

            ViewBag.ogr = ogrenciler;

            List<SelectListItem> dersler = (from i in db.TBLDERSLER.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.DERSAD,
                                                Value = i.DERSID.ToString()
                                            }).ToList();

            ViewBag.drs = dersler;

            var ders = db.TBLNOTLAR.Find(id);

            return View("NotGetir",ders);
        }
        [HttpPost]
        public ActionResult NotGetir(Class1 c1,TBLNOTLAR n,int SINAV1 = 0,int SINAV2 = 0,int SINAV3 = 0,int PROJE = 0)
        {
            List<SelectListItem> ogrenciler = (from i in db.TBLOGRENCILER.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.OGRAD + " " + i.OGRSOYAD,
                                                   Value = i.OGRENCIID.ToString()
                                               }).ToList();

            ViewBag.ogr = ogrenciler;

            List<SelectListItem> dersler = (from i in db.TBLDERSLER.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.DERSAD,
                                                Value = i.DERSID.ToString()
                                            }).ToList();

            ViewBag.drs = dersler;

            if (c1.islem == "Hesapla")
            {
                var sonuc = (SINAV1+ SINAV2+ SINAV3 + PROJE) / 4;
                n.ORTALAMA = sonuc;

                if(sonuc >= 50)
                {
                    n.DURUM = true;
                }
                else
                {
                    n.DURUM = false;
                }

                ModelState.Clear();

                return View(n);
            }

            if(c1.islem == "Guncelle")
            {
                var not = db.TBLNOTLAR.Find(n.NOTID);
                not.OGRID = n.OGRID;
                not.DERSID= n.DERSID;

                not.TBLOGRENCILER = db.TBLOGRENCILER.Find(n.OGRID);
                not.TBLDERSLER = db.TBLDERSLER.Find(n.DERSID);

                not.SINAV1 = n.SINAV1;
                not.SINAV2 = n.SINAV2;
                not.SINAV3 = n.SINAV3;
                not.PROJE = n.PROJE;
                not.ORTALAMA = n.ORTALAMA;
                not.DURUM = n.DURUM; 
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(n);
        }
    }
}