using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class Admin
    {
        public int MaAdmin { get; set; }
        public string UsernameAd { get; set; }
        public string PasswordAd { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string AvatarAd { get; set; }
        public int? Sdt { get; set; }
        public string DiaChi { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string QuocTich { get; set; }

        [NotMapped]
        public IFormFile ChoosePhoto { get; set; }
    }
}
