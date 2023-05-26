using System;
using System.Collections.Generic;

#nullable disable

namespace CongThongTinDHL.Models
{
    public partial class ChuDe
    {
        public ChuDe()
        {
            BaiViets = new HashSet<BaiViet>();
        }

        public string MaChuDe { get; set; }
        public string TenCd { get; set; }
        public string GhiChu { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<BaiViet> BaiViets { get; set; }
    }
}
