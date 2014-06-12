using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePOS.Invoicing
{
    /// <summary>
    /// container for invoice items. uses primitive types only.
    /// </summary>
    public class SaveableInvoiceItem
    {
        #region Attributes
        private double quantity;
        private string number;
        private string name;
        private double price;
        private double sum;
        private double tax;
        #endregion

        public SaveableInvoiceItem (string number, string name, double quantity, double price, double tax)
        {
            this.quantity = quantity;
            this.number = number;
            this.name = name;
            this.price = price;
            this.sum = price * quantity;
            this.tax = tax;
        }

        #region Getter / Setter
        public double Quantity { get { return quantity; } }
        public string Number { get { return number; } }
        public string Name { get { return name; } }
        public double Price { get { return price; } }
        public double Sum { get { return sum; } }
        public double Tax { get { return tax; } }
        #endregion

        public string ToCsvLine()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"").Append(number).Append("\"").Append(",");
            sb.Append("\"").Append(name).Append("\"").Append(",");
            sb.Append("\"").Append(quantity).Append("\"").Append(",");
            sb.Append("\"").Append(price).Append("\"").Append(",");
            sb.Append("\"").Append(sum).Append("\"").Append(",");
            sb.Append("\"").Append(tax).Append("\"");
            return sb.ToString();
        }
    }
}
