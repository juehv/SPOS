using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplePOS.Article
{
    /// <summary>
    /// Container for articles. Can be implemented in diffrent ways like normal article or discount.
    /// </summary>
    public abstract class AbstractArticle
    {
        // implementierte Artikeltypen
        public enum ArticleType
        {
            ARTICLE, DISCOUNT
        }

        protected ArticleType type;
        protected string number;  //Artikelnummer
        protected string name;    //Artikel-Kurzname (für Kassenbon)
        protected double price;   //Preis
        protected double tax; // Steuer

        public AbstractArticle(ArticleType type, string number, string name, double price)
        {
            this.number = number;
            this.name = name;
            this.price = price;
        }

        // TODO remove
        public AbstractArticle(string number) : this(ArticleType.ARTICLE, number, null, 0) { }

        // TODO remove
        public AbstractArticle() : this(null) { }

        /// <summary>
        /// Returns the Article number.
        /// </summary>
        virtual public string Number
        {
            get { return this.number; }
        }

        /// <summary>
        /// Returns the Article name (short for invoice)
        /// </summary>
        virtual public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Returns the Price of the Article
        /// </summary>
        virtual public double Price
        {
            get { return this.price; }
        }

        virtual public ArticleType Type
        {
            get { return type; }
        }

        virtual public double Tax
        {
            get { return tax; }
        }
    }
}
