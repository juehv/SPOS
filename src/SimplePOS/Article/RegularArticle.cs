using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplePOS.Article
{
    /// <summary>
    /// Container for ordinary article
    /// </summary>
    public class RegularArticle : AbstractArticle
    {
        private string text;     //Artikelbeschreibung

        public RegularArticle(string number, string name, string text, double price, double tax)
        {
            base.type = AbstractArticle.ArticleType.ARTICLE;
            base.number = number;
            base.name = name;
            this.text = text;
            base.price = price;
            base.tax = tax;
        }

        // TODO REMOVE
        public RegularArticle(string number) : this(number, "", "", 0, 0) { }
        //TODO REMOVE
        public RegularArticle() : this("","","",0,0) { }

        /// <summary>
        /// Returns the long details text of the article 
        /// </summary>
        virtual public string Text
        {
            get { return this.text; }
        }

        /// <summary>
        /// Returns the article as csv line
        /// </summary>
        /// <returns></returns>
        public string ToCsvLine()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"").Append(number).Append("\"").Append(",");
            sb.Append("\"").Append(name).Append("\"").Append(",");
            sb.Append("\"").Append(text).Append("\"").Append(",");
            sb.Append("\"").Append(price).Append("\"").Append(",");
            sb.Append("\"").Append(tax).Append("\"").Append(",");
            return sb.ToString();
        }

    }
}
