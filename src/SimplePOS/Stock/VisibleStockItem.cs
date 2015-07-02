using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimplePOS.Database;

namespace SimplePOS.Inventory
{
    /// <summary>
    /// Containver for stock item which will be shown in gui. 
    /// Contains additional information about the article.
    /// </summary>
    public class VisibleStockItem : SaveableStockItem
    {
        private string name = "";
        private string text = "";

        public static List<VisibleStockItem> createFromList(ISposDb db)
        {
            List<VisibleStockItem> retval = new List<VisibleStockItem>();
            List<SaveableStockItem> items = db.GetStock();
            foreach (SaveableStockItem item in items){
                retval.Add(new VisibleStockItem(db,item));
            }

            return retval;
        }

        public VisibleStockItem(ISposDb db,SaveableStockItem item) : base(item.Number, item.Quantity)
        {
            SimplePOS.Article.RegularArticle article = db.GetArticleByNumber(item.Number);
            if (article != null)
            {
                this.name = article.Name;
                this.text = article.Text;
            }
        }

        public new string Number {
            get { return base.Number;}
        }

        public string Name
        {
            get { return this.name; }
        }

        public new double Quantity
        {
            get { return base.Quantity; }
        }

        public string Text
        {
            get { return this.text; }
        }

    }
}
