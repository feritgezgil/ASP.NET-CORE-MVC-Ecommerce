#pragma checksum "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "79509e8d64cb008d35b7ae2d4459cdbf20e88be0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_minicart), @"mvc.1.0.view", @"/Views/Shared/minicart.cshtml")]
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
#line 1 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\_ViewImports.cshtml"
using UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\_ViewImports.cshtml"
using UI.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"79509e8d64cb008d35b7ae2d4459cdbf20e88be0", @"/Views/Shared/minicart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"52d79ad08d11418ded2b13adb4a9b2619d15bb23", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_minicart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml"
  
    var Model = new UI.Models.UI.MiniCartModel();
    var ProductCount = Model.Products.Count;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml"
 if (ProductCount > 0)
{
    

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <li class=""nav-item dropdown no-arrow mx-1"">
        <a class=""nav-link dropdown-toggle"" href=""#"" id=""cartDropdown"" role=""button"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
            <i class=""fas fa-shopping-bag fa-fw""></i>
            <!-- Counter - CartItems -->
            <span class=""badge badge-danger badge-counter"">");
#nullable restore
#line 12 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml"
                                                      Write(ProductCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</span>
        </a>
        <!-- Dropdown - CartItems -->
        <div class=""dropdown-list dropdown-menu dropdown-menu-right shadow animated--grow-in"" aria-labelledby=""cartDropdown"">
            <h6 class=""dropdown-header"">
                My Cart
            </h6>
");
#nullable restore
#line 19 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml"
             foreach (var item in Model.Products)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <a class=""dropdown-item d-flex align-items-center"" href=""#"">
                    <div class=""mr-3"">
                        <div class=""icon-circle bg-danger"">
                            <i class=""fa fa-trash text-white""></i>
                        </div>
                    </div>
                    <div>
                        <div class=""small text-gray-500"">");
#nullable restore
#line 28 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml"
                                                    Write(item.name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 29 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml"
                         if (item.is_in_camp)
                        {
                            double productTotalPrice = item.quantity * item.camp_price;


#line default
#line hidden
#nullable disable
            WriteLiteral("                            <span class=\"font-weight-bold\">");
#nullable restore
#line 33 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml"
                                                      Write(productTotalPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 34 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml"
                        }
                        else
                        {
                            double productTotalPrice = item.quantity * item.price;


#line default
#line hidden
#nullable disable
            WriteLiteral("                            <span class=\"font-weight-bold\">");
#nullable restore
#line 39 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml"
                                                      Write(productTotalPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 40 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </a>\r\n");
#nullable restore
#line 43 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <a class=\"dropdown-item text-center small text-gray-500\" href=\"#\">Purchase Cart</a>\r\n        </div>\r\n    </li>\r\n");
#nullable restore
#line 47 "C:\Users\ilkeryolundagerek\source\repos\E-Ticaret\UI\Views\Shared\minicart.cshtml"
}

#line default
#line hidden
#nullable disable
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