#pragma checksum "D:\ShopMayin\MayinOnline\MayinOnline\Views\Home\CreateBill.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "87673c2d75bd63a17818d2efcabc3c5ea6ebb2c3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_CreateBill), @"mvc.1.0.view", @"/Views/Home/CreateBill.cshtml")]
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
#line 1 "D:\ShopMayin\MayinOnline\MayinOnline\Views\_ViewImports.cshtml"
using MayinOnline;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ShopMayin\MayinOnline\MayinOnline\Views\_ViewImports.cshtml"
using MayinOnline.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87673c2d75bd63a17818d2efcabc3c5ea6ebb2c3", @"/Views/Home/CreateBill.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f893f9aa550437b4662514071b58926d967b45bb", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_CreateBill : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MayinOnline.Models.Hoadon>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\ShopMayin\MayinOnline\MayinOnline\Views\Home\CreateBill.cshtml"
  
    ViewData["Title"] = "Đặt hàng thành công";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"trenlar text-center\">\r\n    <h3 class=\"text-success\">Cảm ơn quý khách đã đặt hàng</h3>\r\n    <p>Đơn hàng mã số ");
#nullable restore
#line 8 "D:\ShopMayin\MayinOnline\MayinOnline\Views\Home\CreateBill.cshtml"
                 Write(Model.MaHd);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n    <p>trị giá ");
#nullable restore
#line 9 "D:\ShopMayin\MayinOnline\MayinOnline\Views\Home\CreateBill.cshtml"
           Write(((int)Model.TongTien).ToString("n0"));

#line default
#line hidden
#nullable disable
            WriteLiteral("đ đã được hệ thống ghi nhận. Chúng tôi sẽ sớm liên hệ bạn để xác nhận...</p>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MayinOnline.Models.Hoadon> Html { get; private set; }
    }
}
#pragma warning restore 1591
