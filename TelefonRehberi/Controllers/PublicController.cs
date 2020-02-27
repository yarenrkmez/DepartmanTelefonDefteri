using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TelefonRehberi.Models;

namespace TelefonRehberi.Controllers
{
    public class PublicController : Controller
    {
        // GET: Public
        TelefonDefteriEntities db =new TelefonDefteriEntities();
        public ActionResult Index()
        {
            List<Calisanlar> calisanlars = db.Calisanlars.ToList();
            return View(calisanlars);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calisanlar calisanlar = db.Calisanlars.Find(id);
            if (calisanlar == null)
            {
                return HttpNotFound();
            }
            return View(calisanlar);
        }

    }
}