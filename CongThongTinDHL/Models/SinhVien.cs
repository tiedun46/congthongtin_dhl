using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class SinhVien
    {
        public string MaSv { get; set; }
        public string MaKhoa { get; set; }
        public string MaLop { get; set; }
        public string TenSv { get; set; }
        public string EmailSv { get; set; }
        public string PasswordSv { get; set; }
        public int? Sdt { get; set; }
        public int? SdtphuHuynh { get; set; }
        public string GioiTinh { get; set; }
        public string AnhSv { get; set; }
        public string Cmnd { get; set; }
        public string NoiSinh { get; set; }
        public string DiaChi { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string QuocTich { get; set; }
        public bool Status { get; set; }
        public DateTime? NgaySinh { get; set; }

        public virtual Khoa MaKhoaNavigation { get; set; }
        public virtual Lop MaLopNavigation { get; set; }

        [NotMapped]
        public IFormFile ChoosePhoto { get; set; }
    }
}
