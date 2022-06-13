using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Anmii.Models;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Anmii.Controllers
{
    public class NHAN_VIENController : Controller
    {
        private QLAnmiiEntities db = new QLAnmiiEntities();

        // GET: NHAN_VIEN
        string LayMaMonAn()
        {
            var maMax = db.MON_AN.ToList().Select(n => n.MAMONAN).Max();
            int ma = int.Parse(maMax.Substring(2)) + 1;
            string S = String.Concat("000", ma.ToString());
            return "MM" + S.Substring(ma.ToString().Length - 1);
        }
        //lay ma nhan vien
        string LayMaNV()
        {
            var maMax = db.NHAN_VIEN.ToList().Select(n => n.MANV).Max();
            int MANV = int.Parse(maMax.Substring(2)) + 1;
            string NV = String.Concat("0", MANV.ToString());
            return "NV" + NV.Substring(MANV.ToString().Length - 1);
        }
        public ActionResult Index()
        {
            return View(db.NHAN_VIEN.ToList());
        }

        // GET: NHAN_VIEN/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAN_VIEN nHAN_VIEN = db.NHAN_VIEN.Find(id);
            if (nHAN_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHAN_VIEN);
        }

        // GET: NHAN_VIEN/Create
        public ActionResult Create()
        {
            ViewBag.MANV = LayMaNV();
            return View();
        }

        // POST: NHAN_VIEN/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MANV,HOTENNV,GIOITINH,NGAYSINH,SDT,DIACHI,ANHNV,EMAIL,MATKHAU")] NHAN_VIEN nHAN_VIEN)
        {
            var imgNV = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/" + postedFileName);
            imgNV.SaveAs(path);
            if (ModelState.IsValid)
            {
                nHAN_VIEN.MANV = LayMaNV();
                nHAN_VIEN.ANHNV = postedFileName;
                db.NHAN_VIEN.Add(nHAN_VIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNV = LayMaNV();
            return View(nHAN_VIEN);
        }

        // GET: NHAN_VIEN/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAN_VIEN nHAN_VIEN = db.NHAN_VIEN.Find(id);
            if (nHAN_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHAN_VIEN);
        }

        // POST: NHAN_VIEN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MANV,HOTENNV,GIOITINH,NGAYSINH,SDT,DIACHI,ANHNV,EMAIL,MATKHAU")] NHAN_VIEN nHAN_VIEN)
        {
            var imgNV = Request.Files["Avatar"];
            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Images/" + postedFileName);
                imgNV.SaveAs(path);
            }
            catch
            { }
            if (ModelState.IsValid)
            {
                db.Entry(nHAN_VIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHAN_VIEN);
        }

        // GET: NHAN_VIEN/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAN_VIEN nHAN_VIEN = db.NHAN_VIEN.Find(id);
            if (nHAN_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHAN_VIEN);
        }

        // POST: NHAN_VIEN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHAN_VIEN nHAN_VIEN = db.NHAN_VIEN.Find(id);
            db.NHAN_VIEN.Remove(nHAN_VIEN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult IndexMonAn(string maM = "", string tenM = "", string maTL = "")
        {
            ViewBag.MAMONAN = maM;
            ViewBag.TENMONAN = tenM;
            ViewBag.maTL = new SelectList(db.LOAIMONANs, "MALOAIMONAN", "TENLOAI");

            var monan = db.MON_AN.SqlQuery("Select * from MON_AN where MAMONAN like '%" + maM + "%'"
                + "and TENMONAN like N'%" + tenM + "%'"
                + "and MALOAIMONAN like '%" + maTL + "%'");
            if (monan.Count() == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";
            return View(monan.ToList());
        }
        public ActionResult CreateMonAn()
        {
            ViewBag.MAMONAN = LayMaMonAn();
            ViewBag.MALOAIMONAN = new SelectList(db.LOAIMONANs, "MALOAIMONAN", "TENLOAI");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMonAn(HttpPostedFileBase Avatar, [Bind(Include = "MAMONAN,MALOAIMONAN,TENMONAN,MOTA,ANHMONAN,DONGIA")] MON_AN monan)
        {
            string postedFileName = System.IO.Path.GetFileName(Avatar.FileName);
            var path = System.Web.Hosting.HostingEnvironment.MapPath("/Images/" + postedFileName);
            Avatar.SaveAs(path);

            if (ModelState.IsValid)
            {
                monan.ANHMONAN = postedFileName;
                monan.MAMONAN = LayMaMonAn();
                db.MON_AN.Add(monan);
                db.SaveChangesAsync();
                return RedirectToAction("IndexMonAn");
            }

            ViewBag.MALOAIMONAN = new SelectList(db.LOAIMONANs, "MALOAIMONAN", "TENLOAI", monan.MALOAIMONAN);
            return View(monan);
        }
        public ActionResult EditMonAn(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MON_AN monan = db.MON_AN.Find(id);
            if (monan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MALOAIMONAN = new SelectList(db.LOAIMONANs, "MALOAIMONAN", "TENLOAI", monan.MALOAIMONAN);
            return View(monan);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMonAn([Bind(Include = "MAMONAN,MALOAIMONAN,TENMONAN,MOTA,ANHMONAN,DONGIA")] MON_AN monan)
        {
            var imgNV = Request.Files["Avatar"];
            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgNV.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Images/" + postedFileName);
                imgNV.SaveAs(path);
            }
            catch
            { }

            if (ModelState.IsValid)
            {
                db.Entry(monan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexMonAn");
            }
            ViewBag.MALOAIMONAN = new SelectList(db.LOAIMONANs, "MALOAIMONAN", "TENLOAI", monan.MALOAIMONAN);
            return View(monan);
        }
        public async Task<ActionResult> DeleteMonAn(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MON_AN monan = await db.MON_AN.FindAsync(id);
            if (monan == null)
            {
                return HttpNotFound();
            }
            return View(monan);
        }
        [HttpPost, ActionName("DeleteMonAn")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteMonAnConfirmed(string id)
        {
            MON_AN monan = await db.MON_AN.FindAsync(id);
            db.MON_AN.Remove(monan);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexMonAn");
        }
        //QUẢN LÝ HÓA ĐƠN
        public ActionResult IndexHD()
        {

            return View(db.HOA_DON.ToList());
        }
    
        public async Task<ActionResult> DeleteHD(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOA_DON hoaDon = await db.HOA_DON.FindAsync(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: HoaDons/Delete/5
        [HttpPost, ActionName("DeleteHD")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DelHDConfirmed(string id)
        {
            HOA_DON hoaDon = await db.HOA_DON.FindAsync(id);
            db.HOA_DON.Remove(hoaDon);
            await db.SaveChangesAsync();
            return RedirectToAction("IndexHD");
        }
        [HttpGet]
        public ActionResult CTHoaDon(string id)
        {
            ViewBag.id = id;
            return View();
        }
        //QUẢN LÝ THỐNG KÊ
        public ActionResult ThongKe(DateTime? bd , DateTime? kt)
        {
            ViewBag.bd = bd;
            ViewBag.kt = kt;
            return View(db.HoaDon_ThongKe(bd,kt).ToList());
        }
        public ActionResult ExportToExcel(DateTime? bd, DateTime? kt)
        {
            var thongkes = from m in db.HoaDon_ThongKe(bd, kt)
                          select m;
            String ten = bd + "-" + kt;
            byte[] fileContents;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add(ten);
            Sheet.Cells["C1"].Value = "THỐNG KÊ HÓA ĐƠN";
            Sheet.Cells["A2"].Value = "Từ ngày " + bd;
            Sheet.Cells["A3"].Value = "Đến ngày " + kt;
            Sheet.Cells["A4"].Value = "Mã hóa đơn";
            Sheet.Cells["B4"].Value = "Họ tên khách hàng";
            Sheet.Cells["C4"].Value = "Số điên thoại";
            Sheet.Cells["D4"].Value = "Địa chỉ";
            Sheet.Cells["E4"].Value = "Ngày đặt hàng";
            Sheet.Cells["F4"].Value = "Tổng tiền";
            int sum = 0;
            int row = 5;
            foreach (var item in thongkes)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.MAHD;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.HOTENKH;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.SDT;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.DIACHI;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.THOIGIANDAT?.ToString("dd/MM/yyyy");
                Sheet.Cells[string.Format("F{0}", row)].Value = item.TONGTIEN;
                sum = (int)(sum + item.TONGTIEN);
                row++;
            }
            string Frow = "F" + row;
            Sheet.Cells[Frow].Value = sum + " đồng";
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            fileContents = Ep.GetAsByteArray();

            return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "ThongKeAnmii.xlsx"
            );
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
