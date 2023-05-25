

namespace ReviewFood.Models
{
    using System;
    using System.Collections.Generic;
    
    public   class BaiViet
    {
        
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string HinhAnh { get; set; }
       // public int IdDanhMuc { get; set; }
        public System.DateTime NgayTao { get; set; }
        public System.DateTime NgaySua { get; set; }
        public bool TrangThai { get; set; }
        public int IdTaiKhoan { get; set; }
       
        public int DanhMucId { get; set; }

        public   DanhMuc DanhMuc { get; set; }
        public   TaiKhoan TaiKhoan { get; set; }
        
        public   ICollection<DanhGia> DanhGias { get; set; }
        
    }
}
