using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplePOS.Invoicing
{
    /// <summary>
    /// Class for holding the current invoice.
    /// </summary>
    public class Invoice
    {
        //TODO use preference currency
        private string currency; // Währung
        private long number;    // Rechnungsnummer
        private DateTime date;  // Rechnungsdatum
        private List<InvoiceItem> items;    // Artikel 
        private string pageNumber;   // Seitennummer

        private double taxSet1;
        private double taxSet2;
        private bool showTax;

        #region Constructor
        // Full init
        public Invoice(string currency, long number, DateTime date, List<InvoiceItem> items, string pageNumber, double taxSet1, double taxSet2, bool showTax)
        {
            this.currency = currency;
            this.number = number;
            this.date = date;
            this.items = items;
            this.pageNumber = pageNumber;
            this.taxSet1 = taxSet1;
            this.taxSet2 = taxSet2;
            this.showTax = showTax;
        }

        // Full without currency and page
        public Invoice(long number, DateTime date, List<InvoiceItem> items, double taxSet1, double taxSet2, bool showTax) :
            this(Preferences.PreferenceManager.CURRENCY_TECH, number, date, items, "", taxSet1, taxSet2,showTax) { }

        // Page follower
        public Invoice(Invoice parent, int page, List<InvoiceItem> items, double taxSet1, double taxSet2, bool showTax)
            : this(parent.Number, parent.Date, items, taxSet1, taxSet2,showTax)
        {
            this.pageNumber = this.number + "-" + page;
        }

        // Zero Conf
        public Invoice() : this(-1, DateTime.Now, new List<InvoiceItem>(), Preferences.PreferenceManager.TAX_1, Preferences.PreferenceManager.TAX_2, Preferences.PreferenceManager.SHOW_TAX) { }

        public Invoice Clone()
        {
            return new Invoice(this.currency, this.number, this.date, this.items, this.pageNumber, this.taxSet1, this.taxSet2, this.showTax);
        }
        #endregion

        #region Getter / Setter
        public string Currency
        {
            get { return this.currency; }
            set { this.currency = value; }
        }

        public string PageNumber
        {
            get { if (pageNumber == null || pageNumber == "")return number.ToString(); else return pageNumber; }
            set { pageNumber = value; }
        }

        public DateTime Date
        {
            get { return new DateTime(this.date.Ticks); }
            set { this.date = value; }
        }

        public List<InvoiceItem> Items
        {
            get { return this.items; }
        }

        public long Number
        {
            get { return this.number; }
        }

        public double TaxSet1
        {
            get { return this.taxSet1; }
        }

        public double TaxSet2
        {
            get { return this.taxSet2; }
        }

        public bool ShowTax
        {
            get { return this.showTax; }
        }
        #endregion

        //TODO remove
        public void saveToDb(SimplePOS.Database.ISposDb db)
        {
            number = db.SaveInvoice(new SaveableInvoice(this));
        }

        private void addItem(SimplePOS.Article.AbstractArticle article, double quantity)
        {
            foreach (InvoiceItem item in items)
            {
                if (item.Article.Number == article.Number)
                {
                    item.Quantity++;
                    return; // exit der Funktion
                }
            }
            items.Add(new InvoiceItem(article, quantity));
        }

        private void addDiscount(SimplePOS.Article.AbstractArticle article)
        {
            foreach (InvoiceItem item in items)
            {
                if (SimplePOS.Article.Discount.isDiscountTag(item.Article.Number))
                {
                    ((SimplePOS.Article.Discount)item.Article).RawValue +=
                        ((SimplePOS.Article.Discount)article).RawValue;
                    return; // exit der Funktion
                }
            }
            items.Add(new InvoiceItem(article, 1));
        }

        /// <summary>
        /// adds a item to the invoice
        /// </summary>
        /// <param name="article"></param>
        public void addItem(SimplePOS.Article.AbstractArticle article)
        {
            switch (SimplePOS.Article.ArtikelProcessor.convertToType(article.Number))
            {
                case SimplePOS.Article.AbstractArticle.ArticleType.ARTICLE:
                    addItem(article, 1);
                    return;
                case SimplePOS.Article.AbstractArticle.ArticleType.DISCOUNT:
                    addDiscount(article);
                    return;
            }
        }

        /// <summary>
        /// Returns the article list as name by name.
        /// If there is a quantity > 1 there will be a second line with 
        /// quantity x price
        /// Needed for printing
        /// </summary>
        /// <returns></returns>
        public List<string> getArticleList()
        {
            List<string> retval = new List<string>();
            foreach (InvoiceItem item in items)
            {
                retval.Add(item.Article.Name);
                if (item.Quantity > 1)
                {
                    retval.Add("    " + string.Format("{0:00}", item.Quantity) + " x "
                        + string.Format("{0:0.00}", item.Article.Price) + currency);
                }
            }
            return retval;
        }

        /// <summary>
        /// Returns the prices in order of the article list.
        /// Needed for printing.
        /// </summary>
        /// <returns></returns>
        public List<string> getPriceList()
        {
            List<string> retval = new List<string>();
            StringBuilder sb = new StringBuilder();
            foreach (InvoiceItem item in items)
            {
                if (item.Quantity > 1)
                {
                    retval.Add(" ");
                }
                sb.Clear();
                retval.Add(sb.AppendFormat("{0:0.00}", item.Article.Price * item.Quantity).ToString());
            }
            return retval;
        }

        /// <summary>
        /// Returns the Article list as quantity x name.
        /// Its for display in form.
        /// </summary>
        /// <returns></returns>
        public List<string> getClearArticleList()
        {
            List<string> retval = new List<string>();
            foreach (InvoiceItem item in items)
            {
                retval.Add(string.Format("{0:00}", item.Quantity) + " x "
                        + item.Article.Name);
            }
            return retval;
        }

        /// <summary>
        /// Returns the ammount of the invoice. will be calculated every time you call this function
        /// </summary>
        /// <returns></returns>
        public double getAmmount()
        {
            SimplePOS.Article.ArtikelProcessor.postprocessArtikel(ref items);
            double retval = 0;

            foreach (InvoiceItem item in items)
            {
                retval += item.Quantity * item.Article.Price;
            }

            return retval;
        }

        /// <summary>
        /// Calculates and returns the tax of article with tax 1 (def 19%)
        /// </summary>
        /// <returns></returns>
        public double getTax1()
        {
            double retval = 0;

            foreach (InvoiceItem item in items)
            {
                if (item.Article.Type == Article.AbstractArticle.ArticleType.ARTICLE)
                {
                    double currentTax = ((SimplePOS.Article.RegularArticle)item.Article).Tax;
                    if (currentTax == Preferences.PreferenceManager.TAX_1)
                    {
                        //calc netto
                        double netto = item.Article.Price / (currentTax/100 +1);
                        double tmpTax = item.Article.Price - netto;
                        retval += item.Quantity * tmpTax;
                    }
                }
            }

            return retval;
        }

        /// <summary>
        /// Calculates and returns the tax of article with tax 2 (def 7%)
        /// </summary>
        /// <returns></returns>
        public double getTax2()
        {
            double retval = 0;

            foreach (InvoiceItem item in items)
            {
                if (item.Article.Type == Article.AbstractArticle.ArticleType.ARTICLE)
                {
                    double currentTax = ((SimplePOS.Article.RegularArticle)item.Article).Tax;
                    if (currentTax == Preferences.PreferenceManager.TAX_2)
                    {
                        //calc netto
                        double netto = item.Article.Price / currentTax;
                        double tmpTax = item.Article.Price - netto;
                        retval += item.Quantity * tmpTax;
                    }
                }
            }

            return retval;
        }

        /// <summary>
        /// Caculates the return money.
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public double calculateReturn(double money)
        {
            return money - getAmmount();
        }
    }
}
