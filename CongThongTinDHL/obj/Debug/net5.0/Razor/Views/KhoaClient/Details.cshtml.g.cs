#pragma checksum "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\KhoaClient\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f492924dd9c840c9a56b092cf5f377cc64782ddd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_KhoaClient_Details), @"mvc.1.0.view", @"/Views/KhoaClient/Details.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f492924dd9c840c9a56b092cf5f377cc64782ddd", @"/Views/KhoaClient/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59359790860f1d1a35f8363c90111db3e7586148", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_KhoaClient_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CongThongTinDHL.Models.Khoa>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\KhoaClient\Details.cshtml"
  
    ViewData["Title"] = Model.TenKhoa;
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!-- about section start -->\r\n<div class=\"news_section layout_padding\">\r\n    <div class=\"container\">\r\n        <div class=\"news_section_2 bg-light\">\r\n            <div class=\"row\">\r\n                <div class=\"col-md-12\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 379, "\"", 402, 1);
#nullable restore
#line 14 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\KhoaClient\Details.cshtml"
WriteAttributeValue("", 385, Model.AvatarKhoa, 385, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"image_6\" style=\"width:80%\">\r\n                </div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-md-12\">\r\n                    <div class=\"news_taital_box mb-2\">\r\n                        <h4 class=\"make_text\">");
#nullable restore
#line 20 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\KhoaClient\Details.cshtml"
                                         Write(Model.TenKhoa);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                        <h4 class=\"make_text\">Email: ");
#nullable restore
#line 21 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\KhoaClient\Details.cshtml"
                                                Write(Model.EmailKhoa);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                        <h4 class=\"make_text\">Điện thoại: ");
#nullable restore
#line 22 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\KhoaClient\Details.cshtml"
                                                     Write(Model.Sdtkhoa);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                        <h4 class=\"make_text\">Số giảng viên: ");
#nullable restore
#line 23 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\KhoaClient\Details.cshtml"
                                                        Write(Model.SoGv);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                        <h4 class=\"make_text\">Số sinh viên: ");
#nullable restore
#line 24 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\KhoaClient\Details.cshtml"
                                                       Write(Model.SoGv);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                        <div>");
#nullable restore
#line 25 "D:\Backup\DoAn_CongThongTinDNU\CongThongTinDHL\CongThongTinDHL\Views\KhoaClient\Details.cshtml"
                        Write(Html.Raw(Model.MucTieu));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!-- about section end -->\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CongThongTinDHL.Models.Khoa> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591