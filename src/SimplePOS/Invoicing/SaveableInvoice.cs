using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplePOS.Database;

namespace SimplePOS.Invoicing
{
    /// <summary>
    /// Container class for holding invoice data. Using primitives only.
    /// </summary>
    public class SaveableInvoice
    {
        #region Attributes
        private long date; // Rechnungsdatum
        private long number; // Rechnungsnummer
        private SaveableInvoiceItem[] items; // Artikel
        private double amount; // Rechnungssumme
        private string currency; // Währung (technische)
        #endregion

        #region Contructor
        public SaveableInvoice(long number, long date, SaveableInvoiceItem[] items, double amount, string currency)
        {
            this.number = number;
            this.date = date;
            this.items = items;
            this.amount = amount;
            this.currency = currency;
        }

        public SaveableInvoice(long number, long date, double amount, string currency)
            : this(number, date, new SaveableInvoiceItem[0], amount, currency) { }

        public SaveableInvoice(Invoice invoice)
            : this(invoice.Number, SimplePOS.Util.Timehelper.getTimestampOfDateTime(invoice.Date),
            ConvertInvoiceItems(invoice.Items), invoice.getAmmount(), invoice.Currency)
        {
        }

        #endregion

        #region Getter / Setter
        public long Date { get { return date; } }
        public long Number { get { return number; } set { number = value; } }
        public SaveableInvoiceItem[] Items { get { return items; } set { items = value; } }
        public double Amount { get { return amount; } }
        public string Currency { get { return currency; } }
        #endregion

        #region Private Methods
        private static SaveableInvoiceItem[] ConvertInvoiceItems(List<InvoiceItem> items)
        {
            List<SaveableInvoiceItem> retval = new List<SaveableInvoiceItem>();

            foreach (InvoiceItem item in items)
            {
                retval.Add(new SaveableInvoiceItem(item.Article.Number, item.Article.Name,
                    item.Quantity, item.Article.Price, item.Article.Tax)
                );
            }

            return retval.ToArray();
        }
        #endregion

        #region Public Methods
        public string ToCsvLines()
        {
            StringBuilder sb = new StringBuilder();

            foreach (SaveableInvoiceItem item in items)
            {
                sb.Append("\"").Append(number).Append("\"").Append(",");
                sb.Append("\"").Append(date).Append("\"").Append(",");
                sb.Append("\"").AppendFormat("{0:0.00}", amount).Append("\"").Append(",");
                sb.Append("\"").Append(currency).Append("\"").Append(",");
                sb.Append(item.ToCsvLine());
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
        #endregion
    }
}
