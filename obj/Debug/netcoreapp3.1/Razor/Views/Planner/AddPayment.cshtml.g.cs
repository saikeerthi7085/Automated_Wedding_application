#pragma checksum "F:\Project\code\trunk\Automated_Wedding_Application\Views\Planner\AddPayment.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4697031add290686d29fae5ee68a807be87d2f6e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Planner_AddPayment), @"mvc.1.0.view", @"/Views/Planner/AddPayment.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4697031add290686d29fae5ee68a807be87d2f6e", @"/Views/Planner/AddPayment.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2dfe6f8f8f648844fb45a6c1a42c840426f7a8d2", @"/Views/_ViewImports.cshtml")]
    public class Views_Planner_AddPayment : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Automated_Wedding_Application.Areas.Identity.Data.ApplicationUser>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "F:\Project\code\trunk\Automated_Wedding_Application\Views\Planner\AddPayment.cshtml"
  
    ViewData["Title"] = "AddPayment";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"container\">\r\n");
#nullable restore
#line 7 "F:\Project\code\trunk\Automated_Wedding_Application\Views\Planner\AddPayment.cshtml"
     if (Model.userstripeId == null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h4 class=\"text-center m-5\">Add payment to your profile using stripe</h4>\r\n        <div class=\"justify-content-center text-center\">\r\n            <a class=\"btn btn-primary \"");
            BeginWriteAttribute("href", " href=\"", 423, "\"", 469, 1);
#nullable restore
#line 11 "F:\Project\code\trunk\Automated_Wedding_Application\Views\Planner\AddPayment.cshtml"
WriteAttributeValue("", 430, Url.Action("ConnecttoStrip","Planner"), 430, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Setup Stripe Connect</a>\r\n        </div>\r\n");
#nullable restore
#line 13 "F:\Project\code\trunk\Automated_Wedding_Application\Views\Planner\AddPayment.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h4 class=\"text-center m-5\">Your Payment is set up with Stripe</h4>\r\n        <div class=\"justify-content-center text-center\">\r\n            <h6 class=\"text-center font-weight-bold\">Your Stripe Id is : ");
#nullable restore
#line 18 "F:\Project\code\trunk\Automated_Wedding_Application\Views\Planner\AddPayment.cshtml"
                                                                    Write(Model.userstripeId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n        </div>\r\n");
#nullable restore
#line 20 "F:\Project\code\trunk\Automated_Wedding_Application\Views\Planner\AddPayment.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Automated_Wedding_Application.Areas.Identity.Data.ApplicationUser> Html { get; private set; }
    }
}
#pragma warning restore 1591