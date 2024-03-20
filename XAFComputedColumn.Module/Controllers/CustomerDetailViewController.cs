using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Dashboards;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using XAFComputedColumn.Module.Helper;

namespace XAFComputedColumn.Module.Controllers
{
    public partial class CustomerDetailViewController : ViewController<DetailView>
    {
        public CustomerDetailViewController()
        {
            InitializeComponent();

            TargetObjectType = typeof(BusinessObjects.Customer);
            TargetViewType = ViewType.DetailView;

            PopupWindowShowAction actionCustomerViewDashboard = new PopupWindowShowAction(
                this,
                "OpenCustomerDashboard",
                PredefinedCategory.Edit);

            actionCustomerViewDashboard.ImageName = "BO_Dashboard";
            actionCustomerViewDashboard.Caption = "Analysis";
            actionCustomerViewDashboard.CustomizePopupWindowParams += (sender, args) =>
            {
                try
                {
                    // Find Dashboard and Show it
                    var dashboardDataType = Application.Modules.FindModule<DashboardsModule>().DashboardDataType;
                    var objectSpace = Application.CreateObjectSpace(dashboardDataType);
                    var dashboardData = (IDashboardData)objectSpace.FindObject(
                        dashboardDataType,
                        CriteriaOperator.Parse("[Title] = ?", $"Invoice by Customer"));
                    DetailView dashboardDetailView = Application.CreateDetailView(
                        objectSpace,
                        DashboardsModule.DashboardDetailViewName,
                        true,
                        dashboardData);

                    args.Size = new Size(1200, 800);
                    args.View = dashboardDetailView;
                } catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    args.Context = null;
                }
            };
        }

        protected override void OnActivated() { base.OnActivated(); }
        protected override void OnViewControlsCreated() { base.OnViewControlsCreated(); }
        protected override void OnDeactivated() { base.OnDeactivated(); }
    }
}
