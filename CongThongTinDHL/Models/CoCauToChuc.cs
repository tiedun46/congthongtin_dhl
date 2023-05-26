using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class CoCauToChuc
    {
        public string MaTc { get; set; }
        public string TenTc { get; set; }
        public string Anh { get; set; }
        public string MoTaNgan { get; set; }
        public string MoChiTiet { get; set; }
        public DateTime NgayDang { get; set; }
        public bool Status { get; set; }
        [NotMapped]
        public IFormFile ChoosePhoto { get; set; }
    }
}
