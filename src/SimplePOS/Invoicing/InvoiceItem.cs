using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplePOS.Invoicing
{
    /// <summary>
    /// Container for holding article in the invoice
    /// </summary>
    public class InvoiceItem
    {
        public SimplePOS.Article.AbstractArticle article;
        public double quantity;

        public InvoiceItem(SimplePOS.Article.AbstractArticle article, double quantity)
        {
            this.article = article;
            this.quantity = quantity;
        }

        public SimplePOS.Article.AbstractArticle Article
        {
            get { return this.article; }
        }

        public double Quantity
        {
            get { return this.quantity; }
            set { this.quantity = value; }
        }

        /// <summary>
        /// Returns a string which contains the data in csv format
        /// </summary>
        /// <returns></returns>
        public string ToCsvLine()
        {
            StringBuilder sb = new StringBuilder();
            if (article != null)
            {
                sb.Append(article.Number).Append(",");
                sb.Append(article.Name).Append(",");
                sb.Append("\"").Append(quantity).Append("\"").Append(",");
                sb.Append("\"").Append(article.Price).Append("\"").Append(",");
                sb.Append("\"").Append(article.Price * quantity).Append("\"").Append(",");
            }
            return sb.ToString();
        }

    }
}
