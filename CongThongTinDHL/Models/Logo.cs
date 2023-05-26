using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class Logo
    {
        public int MaLogo { get; set; }
        public string AvatarLogo { get; set; }
        public DateTime NgayTao { get; set; }
        public bool Status { get; set; }

        [NotMapped]
        public IFormFile ChoosePhoto { get; set; }
    }
}
