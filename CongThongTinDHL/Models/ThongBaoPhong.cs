using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class ThongBaoPhong
    {
        public string MaTbphong { get; set; }
        public string MaPhong { get; set; }
        public string TenTb { get; set; }
        public string AnhTb { get; set; }
        public string MoTaNgan { get; set; }
        public string MoChiTiet { get; set; }
        public DateTime NgayDang { get; set; }
        public bool Status { get; set; }

        public virtual Phong MaPhongNavigation { get; set; }

        [NotMapped]
        public IFormFile ChoosePhoto { get; set; }
    }
}
