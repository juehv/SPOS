using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePOS.Invoicing
{
    public class InvoiceItemSimple
    {
        public double quantity;  //Artikelnummer
        public string name;    //Artikel-Kurzname (für Kassenbon)
        public double price;   //Preis
        public string strprice;   //Preis
        public double tax; // Steuer

        public InvoiceItemSimple() {}
        public InvoiceItemSimple(string name, double quantity, double price, string strprice , double tax)
        {
        this.strprice = strprice;
        this.quantity = quantity;
        this.name = name;
        this.price = price;
        this.tax = tax;
        }
    }
}
