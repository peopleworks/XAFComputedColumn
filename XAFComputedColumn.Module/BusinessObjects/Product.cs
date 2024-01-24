
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace XAFComputedColumn.Module.BusinessObjects
{
    [DefaultClassOptions, NavigationItem("Data")]
    public class Product : XPObject
    {
        public Product(Session session) : base(session)
        {
        }
        public override void AfterConstruction() { base.AfterConstruction(); }


        string itemNumber;

        [Size(100)]
        public string ItemNumber
        {
            get => itemNumber;
            set => SetPropertyValue(nameof(ItemNumber), ref itemNumber, value);
        }


        string name;

        [Size(100)]
        public string Name { get => name; set => SetPropertyValue(nameof(Name), ref name, value); }

        double unitPrice;

        public double UnitPrice { get => unitPrice; set => SetPropertyValue(nameof(UnitPrice), ref unitPrice, value); }
    }
}