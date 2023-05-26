using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class GiangVien
    {
        public string MaGv { get; set; }
        public string MaKhoa { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Cmnd { get; set; }
        public string NoiSinh { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
        public string PasswordGv { get; set; }
        public string AnhGv { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string QuocTich { get; set; }
        public bool Status { get; set; }

        public virtual Khoa MaKhoaNavigation { get; set; }
        [NotMapped]
        public IFormFile ChoosePhoto { get; set; }
    }
}
