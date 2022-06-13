using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Anmii.Models;
using PagedList;

namespace Anmii.Controllers
{
    public class MON_AN_NguoiDungController : Controller
    {
        private QLAnmiiEntities db = new QLAnmiiEntities();
        [HttpGet]
        public ActionResult TimKiem(int? page, string param = "")
        {
            if (page == null) page = 1;
            ViewModels mymodel = new ViewModels();
            var nongSans = db.MON_AN.Where(n => n.TENMONAN.Contains(param)).OrderBy(s => s.MAMONAN);
            if (nongSans.Count() == 0)
            {
                ViewBag.Err = "Không có kết quả tìm kiếm nào !";
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var nongSanPagelist = nongSans.ToPagedList(pageNumber, pageSize);
            mymodel.MonAns = nongSanPagelist;
            mymodel.LoaiMonAN = db.LOAIMONANs.ToList();

            return View(mymodel);
        }

        // GET: MON_AN_NguoiDung
        public ActionResult Index(int? page)
        {            
            if (page == null) page = 1;
            ViewModels myModels = new ViewModels();
            var saches = (from i in db.MON_AN
                         select i).OrderBy(x => x.TENMONAN);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var nsPagelist = saches.ToPagedList(pageNumber, pageSize);

            myModels.MonAns = nsPagelist;
            myModels.LoaiMonAN = db.LOAIMONANs.ToList();
            return View(myModels);
        }
        
        // GET: MON_AN_NguoiDung/Details/5
        public ActionResult Details(string id)
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

        // GET: MON_AN_NguoiDung/Create
        public ActionResult Create()
        {
            ViewBag.MALOAIMONAN = new SelectList(db.LOAIMONANs, "MALOAIMONAN", "TENLOAI");
            return View();
        }

        // POST: MON_AN_NguoiDung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAMONAN,MALOAIMONAN,TENMONAN,MOTA,ANHMONAN,DONGIA")] MON_AN mON_AN)
        {
            if (ModelState.IsValid)
            {
                db.MON_AN.Add(mON_AN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MALOAIMONAN = new SelectList(db.LOAIMONANs, "MALOAIMONAN", "TENLOAI", mON_AN.MALOAIMONAN);
            return View(mON_AN);
        }
        

        // GET: MON_AN_NguoiDung/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.MALOAIMONAN = new SelectList(db.LOAIMONANs, "MALOAIMONAN", "TENLOAI", mON_AN.MALOAIMONAN);
            return View(mON_AN);
        }

        // POST: MON_AN_NguoiDung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAMONAN,MALOAIMONAN,TENMONAN,MOTA,ANHMONAN,DONGIA")] MON_AN mON_AN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mON_AN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MALOAIMONAN = new SelectList(db.LOAIMONANs, "MALOAIMONAN", "TENLOAI", mON_AN.MALOAIMONAN);
            return View(mON_AN);
        }

        // GET: MON_AN_NguoiDung/Delete/5
        public ActionResult Delete(string id)
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

        // POST: MON_AN_NguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MON_AN mON_AN = db.MON_AN.Find(id);
            db.MON_AN.Remove(mON_AN);
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
        public ActionResult IndexType(int? page, string Type = "")
        {

            string typeName = "";
            switch (Type)
            {
                case "MA001":
                    typeName = "Bánh mì";
                    break;
                case "MA002":
                    typeName = "Lẩu";
                    break;
                case "MA003":
                    typeName = "Đồ nướng";
                    break;
                case "MA004":
                    typeName = "Nước ngọt";
                    break;
                case "MA005":
                    typeName = "Trà";
                    break;
            }
            ViewBag.Type = typeName;
            if (page == null) page = 1;
            ViewModels mymodel = new ViewModels();
            var saches = db.MON_AN.Include(s => s.LOAIMONAN).Where(n => n.MALOAIMONAN.Contains(Type)).OrderBy(s => s.TENMONAN);

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var nsPagelist = saches.ToPagedList(pageNumber, pageSize);
            mymodel.MonAns = nsPagelist;
            mymodel.LoaiMonAN = db.LOAIMONANs.ToList();
            return View(mymodel);
        }

        public ActionResult IndexDoAn(int? page)
        {
            if (page == null) page = 1;
            ViewModels mymodel = new ViewModels();
            var saches = db.MON_AN.Include(s => s.LOAIMONAN).Where(n => n.MALOAIMONAN.Contains("MA001") || (n.MALOAIMONAN.Contains("MA002")
            )|| (n.MALOAIMONAN.Contains("MA003"))).OrderBy(s => s.TENMONAN);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var nsPagelist = saches.ToPagedList(pageNumber, pageSize);
            mymodel.MonAns = nsPagelist;
            mymodel.LoaiMonAN = db.LOAIMONANs.ToList();
            return View(mymodel);
        }
        public ActionResult IndexDoUong(int? page)
        {
            if (page == null) page = 1;
            ViewModels mymodel = new ViewModels();
            var saches = db.MON_AN.Include(s => s.LOAIMONAN).Where(n => n.MALOAIMONAN.Contains("MA004") || n.MALOAIMONAN.Contains("MA005")
            ).OrderBy(s => s.TENMONAN);
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var nsPagelist = saches.ToPagedList(pageNumber, pageSize);
            mymodel.MonAns = nsPagelist;
            mymodel.LoaiMonAN = db.LOAIMONANs.ToList();
            return View(mymodel);
        }
    }
}
