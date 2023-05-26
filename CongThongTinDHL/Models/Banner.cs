using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class Banner
    {
        public int MaBanner { get; set; }
        public string AnhBanner { get; set; }
        public DateTime? NgayTao { get; set; }
        public bool Status { get; set; }
        public string Link { get; set; }
        public string TenBanner { get; set; }
        public string MoTaNgan { get; set; }

        [NotMapped]
        public IFormFile ChoosePhoto { get; set; }
    }
}
