using System;
using System.Collections.Generic;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class Lop
    {
        public Lop()
        {
            SinhViens = new HashSet<SinhVien>();
        }

        public string MaLop { get; set; }
        public string MaKhoa { get; set; }
        public string TenLop { get; set; }
        public int? Siso { get; set; }
        public string Ghichu { get; set; }
        public bool Status { get; set; }

        public virtual Khoa MaKhoaNavigation { get; set; }
        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}
