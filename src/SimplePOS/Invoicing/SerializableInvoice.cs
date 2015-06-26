using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePOS.Invoicing
{
    public class SerializableInvoice
    {

        //TODO use preference currency
        
        public string currency; // Währung       
        public long number;    // Rechnungsnummer       
        public DateTime date;  // Rechnungsdatum      
        public List<InvoiceItemSimple> items = new List<InvoiceItemSimple>();    // Artikel       
        public string pageNumber;   // Seitennummer       
        public double taxSet1;        
        public double taxSet2;
        public bool showTax;
        public double totalamount;
        public string strtotalamount;
        public string strdate;

        //Konstruktor für Serialisierung
        public SerializableInvoice() {}

        public SerializableInvoice(Invoice invoice) 
        {

            double price = 0;
            string strprice = "0.00";

            this.currency = invoice.currency;
            this.number = invoice.number;
            this.date = invoice.date;
            
            this.pageNumber = invoice.pageNumber;
            this.taxSet1 = invoice.taxSet1;
            this.taxSet2 = invoice.taxSet2;
            this.showTax = invoice.showTax;

            foreach (InvoiceItem item in invoice.items)
            {
               
                price = (item.quantity * item.article.price);
                strprice = price.ToString("0.00");
                items.Add(new InvoiceItemSimple(item.article.name, item.quantity, price, strprice, item.article.tax));
            }

            foreach (InvoiceItemSimple item in items)
            {

                totalamount += item.price;

            }

            strtotalamount = totalamount.ToString("0.00");
            strdate = date.ToString("dd.MM.yyyy");
                    
        }

    }
}
