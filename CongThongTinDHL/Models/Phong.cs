using System;
using System.Collections.Generic;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class Phong
    {
        public Phong()
        {
            ThongBaoPhongs = new HashSet<ThongBaoPhong>();
        }

        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public int? NhanSu { get; set; }
        public string NhiemVu { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<ThongBaoPhong> ThongBaoPhongs { get; set; }
    }
}
