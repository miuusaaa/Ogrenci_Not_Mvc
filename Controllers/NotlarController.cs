using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OgrenciNotMvc.Models.EntityFramework;

namespace OgrenciNotMvc.Controllers
{
    public class NotlarController : Controller
    {
        // GET: Notlar
        DbMvcOkulEntities2 db = new DbMvcOkulEntities2();
        public ActionResult Index()
        {
            var SinavNotlar = db.TBLNOTLAR.ToList();
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
    }
}