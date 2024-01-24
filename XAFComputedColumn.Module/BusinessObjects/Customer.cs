
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace XAFComputedColumn.Module.BusinessObjects
{
    [NavigationItem("Data")]
    [DefaultClassOptions]
    public class Customer : XPObject
    {
        public Customer(Session session) : base(session)
        {
        }

        int customOrder;
        string email;
        bool active;
        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name { get => name; set => SetPropertyValue(nameof(Name), ref name, value); }



        [Size(100)]
        public string Email
        {
            get => email;
            set => SetPropertyValue(nameof(Email), ref email, value);
        }

        public bool Active { get => active; set => SetPropertyValue(nameof(Active), ref active, value); }

        double balance;
        public double Balance
        { get => balance; set => SetPropertyValue(nameof(Balance), ref balance, value); }


        
        public int CustomOrder
        {
            get => customOrder;
            set => SetPropertyValue(nameof(CustomOrder), ref customOrder, value);
        }


    }
}