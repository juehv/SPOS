using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplePOS.Stock
{
    /// <summary>
    /// Container for stock item. Contains primitives only.
    /// </summary>
    public class SaveableStockItem
    {
        private string number;
        private double quantity;

        public SaveableStockItem(string number, double quantity)
        {
            this.number = number;
            this.quantity = quantity;
        }

        public string Number
        {
            get { return number; }
        }

        public double Quantity
        {
            get { return quantity; }
            set { this.quantity = value; }
        }
    }
}
