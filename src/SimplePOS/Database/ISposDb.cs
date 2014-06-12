using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace SimplePOS.Database
{
    public interface ISposDb
    {
        /// <summary>
        /// Inits the db and will create a file if needed. If this fails the
        /// database will not work.
        /// </summary>
        void InitDb();

        /// <summary>
        /// Saves one article to database.
        /// </summary>
        /// <param name="article"></param>
        void SaveArticle(SimplePOS.Article.RegularArticle article);

        /// <summary>
        /// Saves a list of article to database.
        /// </summary>
        /// <param name="articleList"></param>
        void SaveArticleList(List<SimplePOS.Article.RegularArticle> articleList);

        /// <summary>
        /// Deletes one article from database.
        /// </summary>
        /// <param name="number"></param>
        void DeleteArticleByNumber(string number);

        /// <summary>
        /// Requests one article specified by number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        SimplePOS.Article.RegularArticle GetArticleByNumber(string number);

        /// <summary>
        /// Requests all article from database.
        /// </summary>
        /// <returns></returns>
        List<SimplePOS.Article.RegularArticle> GetAllArticles();

        /// <summary>
        /// Deletes one item from stock specified by number
        /// </summary>
        /// <param name="number"></param>
        void DeleteStockItemByNumber(string number);

        /// <summary>
        /// Adds one item to stock. If this item already exists the quantity will be add up.
        /// </summary>
        /// <param name="item"></param>
        void AddItemToStock(SimplePOS.Stock.SaveableStockItem item);

        /// <summary>
        /// Adds one item to stock. If this item already exists the old one
        /// will be replaced.
        /// </summary>
        /// <param name="item"></param>
        void SetItemToStock(SimplePOS.Stock.SaveableStockItem item);

        /// <summary>
        /// Takes the number of quantity articles out of stock.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="quantity"></param>
        void TakeOutOfStock(string number, double quantity);

        /// <summary>
        /// Requests the whole stock.
        /// </summary>
        /// <returns></returns>
        List<SimplePOS.Stock.SaveableStockItem> GetStock();

        /// <summary>
        /// Saves the invoice to the database and returns the invoice number.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        long SaveInvoice(SimplePOS.Invoicing.SaveableInvoice invoice);

        /// <summary>
        /// Requests all invoices.
        /// </summary>
        /// <returns></returns>
        List<SimplePOS.Invoicing.SaveableInvoice> GetAllInvoices();

        /// <summary>
        /// Saves the preferences to database.
        /// </summary>
        /// <param name="pref"></param>
        void SavePreferences();

        /// <summary>
        /// Loads the Preferences from the database.
        /// </summary>
        /// <returns></returns>
        void LoadPreferences();
    }
}
