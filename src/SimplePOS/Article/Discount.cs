using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplePOS.Article
{
    /// <summary>
    /// Class for saving discounts in invoice list.
    /// </summary>
    public class Discount : AbstractArticle
    {
        public const string tagString = "++01";
        private int value;

        public Discount(int value)
        {
            base.type = AbstractArticle.ArticleType.DISCOUNT;
            this.value = value;
            base.tax = 0;
        }

        public Discount()
            : this(0)
        {
        }

        override public string Number
        {
            get { return tagString; }
        }

        override public string Name
        {
            //TODO move "rabatt" to stringdatenbank 
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Rabatt").Append(" ");
                sb.AppendFormat("{0:0.00}", ((double)value / 100));
                return sb.ToString();
            }
        }

        override public double Price
        {
            get
            {
                return -1 * value / 100;
            }
        }

        public int RawValue
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Checks if the given tag is a discount tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static bool isDiscountTag(string tag)
        {
            return tag.StartsWith(tagString);
        }

        /// <summary>
        /// If the Tag has amount data, the method will return this.
        /// Returns -1 else.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static int getAmountOfTag(string tag)
        {
            string amountString = tag.Substring(tagString.Length);
            if (amountString.Length > 0)
            {
                try
                {
                    int amount = Int32.Parse(amountString);
                    return amount;
                }
                catch
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }
    }
}
