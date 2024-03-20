using DevExpress.Xpo;
using System;
using System.Linq;

namespace XAFComputedColumn.Module.BusinessObjects
{
    public class ApplicationUserCurrentActivity : XPObject
    {
        public ApplicationUserCurrentActivity(Session session) : base(session)
        {
        }
        public override void AfterConstruction() { base.AfterConstruction(); }


        string userName;

        public string UserName { get => userName; set => SetPropertyValue(nameof(UserName), ref userName, value); }

        string currentLanguage;

        [Size(20)]
        public string CurrentLanguage
        {
            get => currentLanguage;
            set => SetPropertyValue(nameof(CurrentLanguage), ref currentLanguage, value);
        }

        private Customer customer;

        public Customer Customer
        {
            get { return customer; }
            set => SetPropertyValue(nameof(Customer), ref customer, value);
        }

        DateTime lastUpdate;

        public DateTime LastUpdate
        {
            get => lastUpdate;
            set => SetPropertyValue(nameof(LastUpdate), ref lastUpdate, value);
        }
    }
}