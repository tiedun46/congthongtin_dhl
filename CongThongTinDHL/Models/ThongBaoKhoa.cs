using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class ThongBaoKhoa
    {
        public string MaTbkhoa { get; set; }
        public string MaKhoa { get; set; }
        public string TenTb { get; set; }
        public string AnhTb { get; set; }
        public string MoTaNgan { get; set; }
        public string MoChiTiet { get; set; }
        public DateTime NgayDang { get; set; }
        public bool Status { get; set; }

        public virtual Khoa MaKhoaNavigation { get; set; }

        [NotMapped]
        public IFormFile ChoosePhoto { get; set; }
    }
}
