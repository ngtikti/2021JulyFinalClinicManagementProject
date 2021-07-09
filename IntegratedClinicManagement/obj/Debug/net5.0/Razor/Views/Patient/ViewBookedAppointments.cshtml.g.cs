#pragma checksum "C:\Users\ngtik\Dropbox\dotnet Work\IntegratedClinicManagement\Views\Patient\ViewBookedAppointments.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "409b663d3b3bc80d8330da8eb32c352fbf3540b9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Patient_ViewBookedAppointments), @"mvc.1.0.view", @"/Views/Patient/ViewBookedAppointments.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"409b663d3b3bc80d8330da8eb32c352fbf3540b9", @"/Views/Patient/ViewBookedAppointments.cshtml")]
    public class Views_Patient_ViewBookedAppointments : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ClinicManagementProject.Models.ConsultationDetail>>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\ngtik\Dropbox\dotnet Work\IntegratedClinicManagement\Views\Patient\ViewBookedAppointments.cshtml"
  Layout = "~/views/Patient/_PatientLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<meta name=\"viewport\" content=\"width=device-width\" />\n<title>ViewBookedAppointments</title>\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "409b663d3b3bc80d8330da8eb32c352fbf3540b93071", async() => {
                WriteLiteral("\n\n    <table class=\"table\">\n        <thead>\n            <tr>\n                <th>\n                    ");
#nullable restore
#line 13 "C:\Users\ngtik\Dropbox\dotnet Work\IntegratedClinicManagement\Views\Patient\ViewBookedAppointments.cshtml"
               Write(Html.DisplayNameFor(model => model.Doctor));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                </th>\n                <th>\n                    ");
#nullable restore
#line 16 "C:\Users\ngtik\Dropbox\dotnet Work\IntegratedClinicManagement\Views\Patient\ViewBookedAppointments.cshtml"
               Write(Html.DisplayNameFor(model => model.Timeslot));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                </th>\n                \n                <th>\n                    ");
#nullable restore
#line 20 "C:\Users\ngtik\Dropbox\dotnet Work\IntegratedClinicManagement\Views\Patient\ViewBookedAppointments.cshtml"
               Write(Html.DisplayNameFor(model => model.Consultation_Status));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                </th>\n                \n                <th></th>\n            </tr>\n        </thead>\n        <tbody>\n");
#nullable restore
#line 27 "C:\Users\ngtik\Dropbox\dotnet Work\IntegratedClinicManagement\Views\Patient\ViewBookedAppointments.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("<tr>\n    <td>\n        ");
#nullable restore
#line 31 "C:\Users\ngtik\Dropbox\dotnet Work\IntegratedClinicManagement\Views\Patient\ViewBookedAppointments.cshtml"
   Write(Html.DisplayFor(modelItem => item.Doctor.Name));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n    </td>\n    <td>\n        ");
#nullable restore
#line 34 "C:\Users\ngtik\Dropbox\dotnet Work\IntegratedClinicManagement\Views\Patient\ViewBookedAppointments.cshtml"
   Write(Html.DisplayFor(modelItem => item.Timeslot));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n    </td>\n    \n    <td>\n        ");
#nullable restore
#line 38 "C:\Users\ngtik\Dropbox\dotnet Work\IntegratedClinicManagement\Views\Patient\ViewBookedAppointments.cshtml"
   Write(Html.DisplayFor(modelItem => item.Consultation_Status));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n    </td>\n    \n    <td>\n");
                WriteLiteral("        ");
#nullable restore
#line 43 "C:\Users\ngtik\Dropbox\dotnet Work\IntegratedClinicManagement\Views\Patient\ViewBookedAppointments.cshtml"
   Write(Html.ActionLink("Cancel Appointment", "CancelAppointment", "Patient", new { consultationid = item.Consultation_Id }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n    </td>\n</tr>\n");
#nullable restore
#line 46 "C:\Users\ngtik\Dropbox\dotnet Work\IntegratedClinicManagement\Views\Patient\ViewBookedAppointments.cshtml"
}

#line default
#line hidden
#nullable disable
                WriteLiteral("        </tbody>\n    </table>\n    <section>\n        ");
#nullable restore
#line 50 "C:\Users\ngtik\Dropbox\dotnet Work\IntegratedClinicManagement\Views\Patient\ViewBookedAppointments.cshtml"
   Write(Html.ActionLink("Return to Main Page", "PatientConsole", "Patient"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n    </section>\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</html>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ClinicManagementProject.Models.ConsultationDetail>> Html { get; private set; }
    }
}
#pragma warning restore 1591