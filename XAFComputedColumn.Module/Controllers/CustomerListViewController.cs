using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Linq;
using XAFComputedColumn.Module.BusinessObjects;

namespace XAFComputedColumn.Module.Controllers
{
    public partial class CustomerListViewController : ViewController<ListView>
    {
        SimpleAction saRunStoreProce;
        SimpleAction saRunStoreProceJob;

        public CustomerListViewController()
        {
            InitializeComponent();
            TargetObjectType = typeof(Customer);
            TargetViewType = ViewType.ListView;


            saRunStoreProce = new SimpleAction(this, "RunStoreProce", PredefinedCategory.Unspecified);
            saRunStoreProce.Caption = "Run SP Alone";
            saRunStoreProce.ImageName = "BO_Validation";
            saRunStoreProce.ToolTip = "Run Store Procedure";
            saRunStoreProce.ConfirmationMessage = "Are you sure you want to Run the store procedure?";
            saRunStoreProce.Execute += Run_Store_Procedure;

            saRunStoreProceJob = new SimpleAction(this, "saRunStoreProceJob", PredefinedCategory.Unspecified);
            saRunStoreProceJob.Caption = "Run SP Job";
            saRunStoreProceJob.ImageName = "BO_Validation";
            saRunStoreProceJob.ToolTip = "Run Store Procedure With a Job";
            saRunStoreProceJob.ConfirmationMessage = "Are you sure you want to Run the store procedure with a Job?";
            saRunStoreProceJob.Execute += Run_Store_Procedure;
        }


        private void Run_Store_Procedure(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                MessageOptions options = new MessageOptions();

                Session session = ((XPObjectSpace)ObjectSpace).Session;

                switch (e.Action.Caption)
                {
                    case "Run SP Alone":
                        session.ExecuteSproc("UpdateCustomerCustomOrder");
                        options.Message = "Updated!!";
                        break;
                    case "Run SP Job":
                        session.ExecuteSproc("UpdateCustomerCustomOrderJob");
                        options.Message = "Starting Update, Please Wait...";
                        break;
                }

                View.Refresh();
                View.ObjectSpace.Refresh();



                options.Duration = 3000;
                options.Type = InformationType.Success;
                options.Web.Position = InformationPosition.Bottom;
                Application.ShowViewStrategy.ShowMessage(options);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnActivated() { base.OnActivated(); }
        protected override void OnViewControlsCreated() { base.OnViewControlsCreated(); }
        protected override void OnDeactivated() { base.OnDeactivated(); }
    }
}
