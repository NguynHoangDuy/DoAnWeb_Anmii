//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Anmii.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOA_DON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOA_DON()
        {
            this.CHI_TIET_HOA_HON = new HashSet<CHI_TIET_HOA_HON>();
        }
    
        public string MAHD { get; set; }
        public string HOTENKH { get; set; }
        public string DIACHI { get; set; }
        public string SDT { get; set; }
        public string MANV { get; set; }
        public Nullable<System.DateTime> THOIGIANDAT { get; set; }
        public Nullable<System.DateTime> THOIGIANGIAO { get; set; }
        public Nullable<double> TONGTIEN { get; set; }
    
        public virtual NHAN_VIEN NHAN_VIEN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHI_TIET_HOA_HON> CHI_TIET_HOA_HON { get; set; }
    }
}
