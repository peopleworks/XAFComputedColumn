using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using System;
using System.Linq;
using XAFComputedColumn.Module.BusinessObjects;

namespace XAFComputedColumn.Module.Helper
{
    public static class ApplicationUserHelper
    {
        // Sets
        public static void SetCurrentCustomer(XafApplication application, string currentUserName, int customerOid)
        {
            try
            {
                using(IObjectSpace objectSpace = application.CreateObjectSpace(typeof(ApplicationUserCurrentActivity)))
                {
                    CriteriaOperator criteriaCustomer = CriteriaOperator.FromLambda<Customer>(b => b.Oid == customerOid);

                    Customer customer = objectSpace.FindObject<Customer>(criteriaCustomer);


                    CriteriaOperator criteria = CriteriaOperator.FromLambda<ApplicationUserCurrentActivity>(
                        b => b.UserName == currentUserName);

                    ApplicationUserCurrentActivity u = objectSpace.FindObject<ApplicationUserCurrentActivity>(criteria);

                    if(u == null)
                    {
                        u = objectSpace.CreateObject<ApplicationUserCurrentActivity>();

                        u.UserName = currentUserName;
                        u.Customer = customer;
                        u.LastUpdate = DateTime.Now;
                    } else
                    {
                        u.Customer = customer;
                        u.LastUpdate = DateTime.Now;
                    }
                    objectSpace.CommitChanges();
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Gets
        public static string GetCurrentLanguage(XafApplication application)
        {
            try
            {
                string currentUserName = SecuritySystem.CurrentUserName;

                using(IObjectSpace objectSpace = application.CreateObjectSpace(typeof(ApplicationUserCurrentActivity)))
                {
                    CriteriaOperator criteria = CriteriaOperator.FromLambda<ApplicationUserCurrentActivity>(
                        b => b.UserName == currentUserName);

                    var setting = objectSpace.FindObject<ApplicationUserCurrentActivity>(criteria);

                    return setting != null ? setting.CurrentLanguage : string.Empty;
                }
            } catch(Exception)
            {
                return string.Empty;
            }
        }

        public static Customer GetCurrentCustomer(UnitOfWork uow, string currentUserName)
        {
            try
            {
                var querySetting = uow.Query<ApplicationUserCurrentActivity>()
                    .OrderByDescending(q => q.LastUpdate)
                    .Where(q => string.IsNullOrEmpty(currentUserName) || q.UserName == currentUserName)
                    .Select(s => new { s.UserName, s.CurrentLanguage, s.LastUpdate, s.Customer })
                    .Take(1);

                return querySetting.FirstOrDefault()?.Customer;
            } catch(Exception)
            {
                return null;
            }
        }
    }
}
