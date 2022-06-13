using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anmii.Models;

namespace Anmii.Controllers
{
    public class GioHangController : Controller
    {
        QLAnmiiEntities db = new QLAnmiiEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        [HttpPost]
        public ActionResult DatHang(string tenKH = "", string sdt = "", string diaChi = "")
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HOA_DON hd = new HOA_DON();
                List<GioHang> gh = LayGioHang();

                ViewBag.tenKH = tenKH;
                ViewBag.diaChi = diaChi;
                ViewBag.sdt = sdt;
                hd.MAHD = LayMaHD();
                //ngay mua hang.
                hd.THOIGIANDAT = DateTime.Now;
                hd.THOIGIANGIAO = DateTime.Now.AddHours(+2);
                hd.HOTENKH = tenKH.ToString();
                hd.SDT = sdt.ToString();
                hd.DIACHI = diaChi.ToString();
                hd.MANV = "NV01";
                hd.TONGTIEN = TongTien();
                db.HOA_DON.Add(hd);
                db.SaveChanges();
                foreach (var item in gh)
                {
                    CHI_TIET_HOA_HON cthd = new CHI_TIET_HOA_HON();
                    cthd.MAHD = hd.MAHD;
                    cthd.MAMONAN = item.iMaMon;
                    cthd.SOLUONG = item.iSoLuong;
                    db.CHI_TIET_HOA_HON.Add(cthd);
                }
                db.SaveChanges();
                List<GioHang> lstGioHang = LayGioHang();
                lstGioHang.Clear();
                return RedirectToAction("Index", "MON_AN_NguoiDung");
            }
        }
        public ActionResult ThemGioHang(string Ma, string strURL)
        {
            MON_AN sach = db.MON_AN.SingleOrDefault(n => n.MAMONAN == Ma);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang gh = lstGioHang.Find(n => n.iMaMon == Ma);
            if (gh == null)
            {
                gh = new GioHang(Ma);
                lstGioHang.Add(gh);
                return Redirect(strURL);
            }
            else
            {
                gh.iSoLuong++;
                return Redirect(strURL);
            }
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();

            return View(lstGioHang);
        }
        public double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongTien = lstGioHang.Sum(n => n.iThanhTien);
            }
            return iTongTien;
        }
        public ActionResult CapNhatGioHang(string Ma, FormCollection f)
        {
            MON_AN sach = db.MON_AN.SingleOrDefault(n => n.MAMONAN == Ma);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaMon == Ma);
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"]);
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang(string Ma)
        {
            MON_AN sach = db.MON_AN.SingleOrDefault(n => n.MAMONAN == Ma);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaMon == Ma);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaMon == Ma);
                return RedirectToAction("GioHang", "Giohang");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("GioHang", "Giohang");
            }
            return RedirectToAction("GioHang", "Giohang");
        }
        public ActionResult SuaGioHang()
        {

            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }
        public int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            else
            {
                iTongSoLuong = 0;
            }
            return iTongSoLuong;
        }
        public PartialViewResult TB()
        {
            if (TongSoLuong() == 0)
            {
                ViewBag.TB = "Không có gì trong giỏ hàng";
            }
            else
            {
                ViewBag.TB = "";
            }
            return PartialView();
        }

        string LayMaHD()
        {
            var maMax = db.HOA_DON.ToList().Select(n => n.MAHD).Max();
            int maHD = int.Parse(maMax.Substring(2)) + 1;
            string HD = String.Concat("00", maHD.ToString());
            return "HD" + HD.Substring(maHD.ToString().Length - 1);
        }

    }   
}