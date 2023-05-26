using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class Khoa
    {
        public Khoa()
        {
            GiangViens = new HashSet<GiangVien>();
            Lops = new HashSet<Lop>();
            SinhViens = new HashSet<SinhVien>();
            ThongBaoKhoas = new HashSet<ThongBaoKhoa>();
        }

        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public string AvatarKhoa { get; set; }
        public string EmailKhoa { get; set; }
        public int? Sdtkhoa { get; set; }
        public int? SoGv { get; set; }
        public int? SoSv { get; set; }
        public string MucTieu { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<GiangVien> GiangViens { get; set; }
        public virtual ICollection<Lop> Lops { get; set; }
        public virtual ICollection<SinhVien> SinhViens { get; set; }
        public virtual ICollection<ThongBaoKhoa> ThongBaoKhoas { get; set; }

        [NotMapped]
        public IFormFile ChoosePhoto { get; set; }
    }
}
