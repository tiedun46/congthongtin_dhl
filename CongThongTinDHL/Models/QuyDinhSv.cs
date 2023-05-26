using System;
using System.Collections.Generic;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class QuyDinhSv
    {
        public string MaQdsv { get; set; }
        public string TenQd { get; set; }
        public string MoTaNgan { get; set; }
        public string MoChiTiet { get; set; }
        public DateTime NgayDang { get; set; }
        public bool Status { get; set; }
    }
}
