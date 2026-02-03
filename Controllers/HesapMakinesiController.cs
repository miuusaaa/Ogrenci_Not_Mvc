using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciNotMvc.Controllers
{
    public class HesapMakinesiController : Controller
    {
        // GET: HesapMakinesi
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string sayi1,string sayi2,string islem)
        {
            double n1, n2;

            if (string.IsNullOrWhiteSpace(sayi1) || string.IsNullOrWhiteSpace(sayi2))
            {
                ViewBag.Hata = "Hesaplanacak sayıları giriniz.";
                return View();
            }

            if (!double.TryParse(sayi1.Replace(".", ","), out n1) ||
                !double.TryParse(sayi2.Replace(".", ","), out n2))
            {
                ViewBag.Hata = "Geçerli iki sayı giriniz.";
                return View();
            }

            switch (islem)
            {
                
                case "topla":
                    ViewBag.snc= n1+ n2;
                    break;

                case "cikar":
                    ViewBag.snc = n1-n2;
                    break;

                case "carp":
                    ViewBag.snc = n1*n2;
                    break;

                case "bol":
                    if (n2 == 0)
                    {
                        ViewBag.Hata = "0'a bölme yapılamaz.";
                    }
                    else
                    {
                        ViewBag.snc= n1/n2;
                    }
                    break;
            }
            return View();
        }
    }
}