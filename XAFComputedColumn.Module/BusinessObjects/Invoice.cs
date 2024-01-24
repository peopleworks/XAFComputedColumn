using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace XAFComputedColumn.Module.BusinessObjects
{
    [NavigationItem("Data")]
    [DefaultClassOptions]
    public class Invoice : XPObject
    {
        public Invoice(Session session) : base(session)
        {
        }
        Customer customer;
        DateTime date;

        public DateTime Date { get => date; set => SetPropertyValue(nameof(Date), ref date, value); }

        public Customer Customer { get => customer; set => SetPropertyValue(nameof(Customer), ref customer, value); }


        public double TotalAmount
        {
            get
            {
                try
                {
                    double result = 0;
                    result = InvoiceDetails.Where(w => w.Quantity > 0).Sum(s => s.Quantity * s.UnitPrice);
                    return result;
                } catch(Exception)
                {
                    return 0;
                }
            }
        }


        double totalDiscount;

        public double TotalDiscount
        {
            get => totalDiscount;
            set => SetPropertyValue(nameof(TotalDiscount), ref totalDiscount, value);
        }


        [Association("Invoice-InvoiceDetails"), Aggregated]
        public XPCollection<InvoiceDetail> InvoiceDetails
        {
            get { return GetCollection<InvoiceDetail>(nameof(InvoiceDetails)); }
        }
    }


    //[NavigationItem("Data")]
    public class InvoiceDetail : XPObject
    {
        public InvoiceDetail(Session session) : base(session)
        {
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            try
            {
                switch(propertyName)
                {
                    case nameof(Product):
                        if(!ReferenceEquals(null, this.Invoice))
                        {
                            if(this.UnitPrice == 0)
                            {
                                this.UnitPrice = this.Product.UnitPrice;
                            }
                        }
                        break;
                    default:
                        break;
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        Product product;

        [ImmediatePostData]
        public Product Product { get => product; set => SetPropertyValue(nameof(Product), ref product, value); }

        public string ProductName => Product?.Name;

        int quantity;

        public int Quantity { get => quantity; set => SetPropertyValue(nameof(Quantity), ref quantity, value); }

        double unitPrice;

        public double UnitPrice { get => unitPrice; set => SetPropertyValue(nameof(UnitPrice), ref unitPrice, value); }


        Invoice invoice;

        [Association("Invoice-InvoiceDetails")]
        public Invoice Invoice { get => invoice; set => SetPropertyValue(nameof(Invoice), ref invoice, value); }
    }
}