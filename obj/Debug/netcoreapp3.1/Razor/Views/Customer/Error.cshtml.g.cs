#pragma checksum "F:\Project\code\trunk\Automated_Wedding_Application\Views\Customer\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "75e7efcf80dae881cf4d7ce2c6756ee7228dc68d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_Error), @"mvc.1.0.view", @"/Views/Customer/Error.cshtml")]
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
#line 1 "F:\Project\code\trunk\Automated_Wedding_Application\Views\_ViewImports.cshtml"
using Automated_Wedding_Application;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\Project\code\trunk\Automated_Wedding_Application\Views\_ViewImports.cshtml"
using Automated_Wedding_Application.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"75e7efcf80dae881cf4d7ce2c6756ee7228dc68d", @"/Views/Customer/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2dfe6f8f8f648844fb45a6c1a42c840426f7a8d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "F:\Project\code\trunk\Automated_Wedding_Application\Views\Customer\Error.cshtml"
  
    ViewData["Title"] = "Error";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"container m-3\">\r\n    <h5 class=\"text-center text-danger m-3\"> Transaction is failed Check below Error and Retry</h5>\r\n\r\n    <h6 class=\"text-center text-danger m-3\"> ");
#nullable restore
#line 9 "F:\Project\code\trunk\Automated_Wedding_Application\Views\Customer\Error.cshtml"
                                        Write(ViewData["error"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n    <div class=\"text-center\">\r\n        <a class=\"btn  text-center m-1\" id=\"bgcolor1\"");
            BeginWriteAttribute("href", " href=", 381, "", 428, 1);
#nullable restore
#line 11 "F:\Project\code\trunk\Automated_Wedding_Application\Views\Customer\Error.cshtml"
WriteAttributeValue("", 387, Url.Action("GetCartDetails", "Customer"), 387, 41, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">GoBack</a>\r\n    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591