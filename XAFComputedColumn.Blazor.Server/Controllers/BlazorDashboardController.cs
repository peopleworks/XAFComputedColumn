using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Dashboards.Blazor.Components;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using Microsoft.JSInterop;
using System;
using System.Linq;
using XAFComputedColumn.Module.BusinessObjects;
using XAFComputedColumn.Module.Helper;

namespace XAFComputedColumn.Blazor.Server.Controllers
{
    public partial class BlazorDashboardController : ObjectViewController<DetailView, IDashboardData>
    {
        private IJSRuntime JSRuntime { get; set; }

        private DotNetObjectReference<BlazorDashboardController> controllerReference;

        /// <summary>
        /// <para>Creates an instance of the <see cref="BlazorDashboardController`2"/> class.</para>
        /// </summary>
        public BlazorDashboardController()
        {
            TargetObjectType = typeof(IDashboardData);
            controllerReference = DotNetObjectReference.Create(this);
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            View.CustomizeViewItemControl<BlazorDashboardViewerViewItem>(this, CustomizeDashboardViewerViewItem);


            // Add Js References
            var application = (BlazorApplication)Application;
            JSRuntime = application.ServiceProvider.GetRequiredService<IJSRuntime>();
            _ = JSRuntime.InvokeVoidAsync("customScript.registerController", controllerReference).Preserve();
        }

        /// <summary>
        /// Customize Dashboard Parameters and Appearance
        /// </summary>
        /// <param name="blazorDashboardViewerViewItem"></param>
        void CustomizeDashboardViewerViewItem(BlazorDashboardViewerViewItem blazorDashboardViewerViewItem)
        {
            if(blazorDashboardViewerViewItem.Control is DxDashboardViewerAdapter dxDashboardViewerAdapter)
            {
                // Set parameters for Dashboard
                IDashboardData dashboard = blazorDashboardViewerViewItem.CurrentObject as IDashboardData;
                var dashboardState = new DashboardState();


                switch(dashboard.Title)
                {
                    case "Invoice by Customer":

                        Customer customer = null;

                        // Verify main Window
                        if(Application.MainWindow.View != null &&
                            Application.MainWindow.View.Id == "Customer_DetailView")
                        {
                            customer = Application.MainWindow.View.CurrentObject as Customer;
                        }


                        int parameterList = 0;
                        if(customer != null)
                        {
                            parameterList = customer.Oid;
                        }

                        dashboardState.Parameters
                            .Add(new DashboardParameterState("CustomerID", parameterList, typeof(int)));

                        break;
                    default:
                        break;
                }

                // Apply Dashboard State
                dxDashboardViewerAdapter.ComponentModel.InitialDashboardState = dashboardState.SaveToJson();
            }
        }
    }
}
