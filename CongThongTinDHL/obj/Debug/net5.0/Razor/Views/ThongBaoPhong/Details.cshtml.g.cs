#pragma checksum "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\ThongBaoPhong\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7120425c68377febe83784772bd6dee987c7b1c3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ThongBaoPhong_Details), @"mvc.1.0.view", @"/Views/ThongBaoPhong/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\_ViewImports.cshtml"
using CongThongTinDHL;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\_ViewImports.cshtml"
using CongThongTinDHL.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7120425c68377febe83784772bd6dee987c7b1c3", @"/Views/ThongBaoPhong/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59359790860f1d1a35f8363c90111db3e7586148", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ThongBaoPhong_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CongThongTinDHL.Models.ThongBaoPhong>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\ThongBaoPhong\Details.cshtml"
  
    ViewData["Title"] = "Thông báo phòng";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<!-- about section start -->\r\n<div class=\"news_section layout_padding\">\r\n    <div class=\"container\">\r\n        <div class=\"news_section_2 bg-light\">\r\n            <div class=\"row\">\r\n                <div class=\"col-md-12\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 390, "\"", 408, 1);
#nullable restore
#line 13 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\ThongBaoPhong\Details.cshtml"
WriteAttributeValue("", 396, Model.AnhTb, 396, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"image_6\" style=\"width:80%\">\r\n                </div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-md-12\">\r\n                    <div class=\"news_taital_box mb-2\">\r\n                        <p class=\"date_text\">");
#nullable restore
#line 19 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\ThongBaoPhong\Details.cshtml"
                                        Write(Model.NgayDang);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <h4 class=\"make_text\">");
#nullable restore
#line 20 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\ThongBaoPhong\Details.cshtml"
                                         Write(Model.TenTb);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                        <h4 class=\"make_text\">");
#nullable restore
#line 21 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\ThongBaoPhong\Details.cshtml"
                                         Write(Model.MaPhongNavigation.TenPhong);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                        <p class=\"lorem_text\">");
#nullable restore
#line 22 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\ThongBaoPhong\Details.cshtml"
                                         Write(Model.MoTaNgan);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <div>");
#nullable restore
#line 23 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\ThongBaoPhong\Details.cshtml"
                        Write(Html.Raw(Model.MoChiTiet));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!-- about section end -->");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CongThongTinDHL.Models.ThongBaoPhong> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
