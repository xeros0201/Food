using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewFood.Models
{
    public class DanhGia_BaiViet
    {
        public string NoiDung { get; set; }
        public BaiViet BaiViet { get; set; }
        public string TaiKhoan { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgaySua { get; set; }
        public bool TrangThai { get; set; }
    }
}