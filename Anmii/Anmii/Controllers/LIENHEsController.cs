using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Anmii.Models;

namespace Anmii.Controllers
{
    public class LIENHEsController : Controller
    {
        private QLAnmiiEntities db = new QLAnmiiEntities();

        // GET: LIENHEs
        public ActionResult Index()
        {
            return View(db.LIENHEs.ToList());
        }

        // GET: LIENHEs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIENHE lIENHE = db.LIENHEs.Find(id);
            if (lIENHE == null)
            {
                return HttpNotFound();
            }
            return View(lIENHE);
        }

        // GET: LIENHEs/Create
        public ActionResult Create(String HOTEN = "", String SDT = "", String EMAIL = "", String TIEUDE = "", String NOIDUNG = "")
        {
            LIENHE lIENHE = new LIENHE();
            lIENHE.MALIENHE = LayMaLH();
            lIENHE.HOTEN = HOTEN;
            lIENHE.SDT = SDT;
            lIENHE.EMAIL = EMAIL;
            lIENHE.TIEUDE = TIEUDE;
            lIENHE.NOIDUNG = NOIDUNG;
            db.LIENHEs.Add(lIENHE);
            db.SaveChanges();
            return View();
        }
        string LayMaLH()
        {
            var maMax = db.LIENHEs.ToList().Select(n => n.MALIENHE).Max();
            int maLH = int.Parse(maMax.Substring(2)) + 1;
            string LH = String.Concat("000", maLH.ToString());
            return "LH" + LH.Substring(maLH.ToString().Length - 1);
        }

        // GET: LIENHEs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIENHE lIENHE = db.LIENHEs.Find(id);
            if (lIENHE == null)
            {
                return HttpNotFound();
            }
            return View(lIENHE);
        }

        // POST: LIENHEs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MALIENHE,HOTEN,SDT,EMAIL,TIEUDE,NOIDUNG")] LIENHE lIENHE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lIENHE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lIENHE);
        }

        // GET: LIENHEs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LIENHE lIENHE = db.LIENHEs.Find(id);
            if (lIENHE == null)
            {
                return HttpNotFound();
            }
            return View(lIENHE);
        }

        // POST: LIENHEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LIENHE lIENHE = db.LIENHEs.Find(id);
            db.LIENHEs.Remove(lIENHE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
