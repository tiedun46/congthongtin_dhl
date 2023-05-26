using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class BaiViet
    {
        public int MaBv { get; set; }
        public string MaChuDe { get; set; }
        public string TenBv { get; set; }
        public string AnhBv { get; set; }
        public string MoTaNgan { get; set; }
        public string MoChiTiet { get; set; }
        public DateTime NgayDang { get; set; }
        public bool Status { get; set; }

        public virtual ChuDe MaChuDeNavigation { get; set; }

        [NotMapped]
        public IFormFile ChoosePhoto { get; set; }
    }
}
