using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Anmii.Models
{
    public class ViewModels
    {
        public IPagedList<MON_AN> MonAns { get; set; }
        public IEnumerable<LOAIMONAN> LoaiMonAN { get; set; }
        public MON_AN mon_an { get; set; }
        public HOA_DON hoaDon { get; set; }
        public NHAN_VIEN nhanVien { get; set; }
        public CHI_TIET_HOA_HON cthd { get; set; }
        public NHAN_VIEN nv { get; set; }
    }
}