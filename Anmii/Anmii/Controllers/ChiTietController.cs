using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Anmii.Models;

namespace Anmii.Controllers
{
    public class ChiTietController : Controller
    {
        private QLAnmiiEntities db = new QLAnmiiEntities();
        // GET: ChiTiet
        public ActionResult Details(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MON_AN mON_AN = db.MON_AN.Find(id);
            if (mON_AN == null)
            {
                return HttpNotFound();
            }
            return View(mON_AN);
        }

        public ActionResult GioiThieu()
        {
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }
        string LayMaHD()
        {
            var maMax = db.LIENHEs.ToList().Select(n => n.MALIENHE).Max();
            int maLH = int.Parse(maMax.Substring(2)) + 1;
            string LH = String.Concat("000", maLH.ToString());
            return "LH" + LH.Substring(maLH.ToString().Length - 1);
        }
    }
}