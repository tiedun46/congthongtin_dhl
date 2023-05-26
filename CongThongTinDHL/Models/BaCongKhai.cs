using System;
using System.Collections.Generic;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class BaCongKhai
    {
        public string MaBck { get; set; }
        public string TenBck { get; set; }
        public string Link { get; set; }
        public string MoTaNgan { get; set; }
        public string MoChiTiet { get; set; }
        public DateTime NgayDang { get; set; }
        public bool Status { get; set; }
    }
}
