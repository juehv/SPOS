using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePOS.InventoryItem
{
    public class InventoryItem
    {
        public string name;
        public string number;
        public double quantity;
        public double actual_stock;

        public InventoryItem(string name, string number, double quantity, double actual_stock)
        {
            this.name = name;
            this.number = number;
            this.quantity = quantity;
            this.actual_stock = actual_stock;
        }
        virtual public string Name
        {
            get { return this.name; }
        }

        virtual public string Artikelnummer
        {
            get { return this.number; }
        }

        virtual public double SollBestand
        {
            get { return this.quantity; }
        }

        virtual public double IstBestand
        {
            get { return this.actual_stock; }
        }

    }
}
