using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anmii.Models
{
    public class GioHang
    {
        QLAnmiiEntities db = new QLAnmiiEntities();
        public string iMaMon { get; set; }
        public string iTenMon { get; set; }
        public string iAnh { get; set; }
        public int iDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double iThanhTien { get { return iSoLuong * iDonGia; } }

        public GioHang(string Ma)
        {
            iMaMon = Ma;
            MON_AN monAn = db.MON_AN.SingleOrDefault(n => n.MAMONAN == iMaMon);
            iTenMon = monAn.TENMONAN;
            iAnh = monAn.ANHMONAN;
            iDonGia = (int)decimal.Parse(monAn.DONGIA.ToString());
            iSoLuong = 1;
        }
    }
}
