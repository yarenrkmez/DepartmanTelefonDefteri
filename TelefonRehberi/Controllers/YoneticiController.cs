using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TelefonRehberi.App_Classes;
using TelefonRehberi.Models;

namespace TelefonRehberi.Controllers
{
    public class YoneticiController : Controller
    {
        // GET: Yonetici
        TelefonDefteriEntities db = new TelefonDefteriEntities();
        
        public ActionResult Index()
        {
            CalisanlarDepartman k = new CalisanlarDepartman();
            k.calisanlars = db.Calisanlars.ToList();
            k.departmanlars = db.Departmanlars.ToList();
            
            k.yoneticis = db.Yoneticis.ToList();
           
            if (User.Identity.IsAuthenticated)
            {
                Yonetici y = db.Yoneticis.FirstOrDefault(x => x.Email == User.Identity.Name); ViewData["departmanid"] = y.DepartmanID;
         
            }

            return View(k);

        }
        [HttpPost]

        public ActionResult GirisYap(string Email, string Sifre)
        {
            Yonetici y = db.Yoneticis.ToList().Where(x => x.Email.Equals(Email) && x.Sifre.Equals(Sifre)).FirstOrDefault();



            if (y != null)
            {
                FormsAuthentication.SetAuthCookie(y.Email, true);

             
                return RedirectToAction("Index", "Yonetici");
            }



            return RedirectToAction("Index", "Yonetici");

        }
        [HttpPost]
        public ActionResult CalisanEkle(Calisanlar c)
        {
            db.Calisanlars.Add(c);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult SifreDegistir(Yonetici y)
        {
            Yonetici yönetici = db.Yoneticis.FirstOrDefault(x => x.Email == User.Identity.Name);
            yönetici.Sifre = y.Sifre;


            db.SaveChanges();

            //return RedirectToAction("Index");
            return Json("başarılı", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void CalisanSil(int id)
        {
            Calisanlar u = db.Calisanlars.FirstOrDefault(x => x.CalisID == id);
            db.Calisanlars.Remove(u);

            db.SaveChanges();
        }
        public ActionResult Guncelle(int id)
        {
            List<Calisanlar> calisanlars = db.Calisanlars.ToList();

            ViewData["Calisanid"] = id;
            return View(calisanlars);
        }
        [HttpPost]
        public ActionResult Guncelle(Calisanlar c,int id)
        {
            Calisanlar u = db.Calisanlars.FirstOrDefault(x => x.CalisID == id);
            u.Adi = c.Adi;
            u.Soyadi = c.Soyadi;
            u.TelefonNo = u.TelefonNo;
            db.SaveChanges();
           return RedirectToAction("Index");
        }
        [HttpPost]
public ActionResult DepEkle(Departmanlar d)
        {
            db.Departmanlars.Add(d);
  
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DepGuncelle(int id)
        {
            List<Departmanlar> departmanlars = db.Departmanlars.ToList();

            ViewData["Depid"] = id;
            return View(departmanlars);
        }
        [HttpPost]
        public ActionResult DepGuncelle(Departmanlar d, int id)
        {
            Departmanlar u = db.Departmanlars.FirstOrDefault(x => x.DepartmanID == id);
            u.DepAdi = d.DepAdi;
            u.DepartmanBilgi = d.DepartmanBilgi;
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void DepSil(int id)
        {
            Calisanlar c=db.Calisanlars.FirstOrDefault(x => x.DepartmanID == id);
            Departmanlar d = db.Departmanlars.FirstOrDefault(x => x.DepartmanID == id);
       
            if (c !=null && c.DepartmanID.ToString()==d.DepartmanID.ToString() )
            {
                 RedirectToAction("Index");
            }
            else
            {
      db.Departmanlars.Remove(d);

            db.SaveChanges();
            }
      
        }
        public ActionResult SifremiUnuttum()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult SifremiUnuttum(Yonetici y)
        {
            Yonetici yo = db.Yoneticis.FirstOrDefault(x => x.Email ==y.Email );
            if (yo.GizliSoru==y.GizliSoru &&yo.GizliCevap==y.GizliCevap)
            {
                yo.Sifre = y.Sifre;
     db.SaveChanges();
                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }

    }
}