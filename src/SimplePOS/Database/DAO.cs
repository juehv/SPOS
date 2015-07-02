using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using SimplePOS.Article;
using SimplePOS.Invoicing;
using SimplePOS.Inventory;


namespace SimplePOS.Database
{
    class DAO : ISposDb
    {
        private static DAO INSTANCE = null;
        private static string DB_FILNAME = "spos.db";

        private DAO()
        {
            //empty
        }

        public static DAO getInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new DAO();
            }
            return INSTANCE;
        }

        public void InitDb()
        {
            IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME);
            db.Close();
        }

        public void SaveArticleList(List<SimplePOS.Article.RegularArticle> articleList)
        {
            foreach (SimplePOS.Article.RegularArticle item in articleList)
            {
                SaveArticle(item);
            }
        }

        public void SaveArticle(SimplePOS.Article.RegularArticle article)
        {
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    if (getArticleByNumberCore(article.Number, db) != null)
                    {
                        // alte Artikel Löschen
                        deleteArticleByNumberCore(article.Number, db);
                    }
                    db.Store(article);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        private void deleteArticleByNumberCore(string number, IObjectContainer db)
        {
            db.Delete(getArticleByNumberCore(number, db));
        }

        public void DeleteArticleByNumber(string number)
        {
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    deleteArticleByNumberCore(number, db);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        private SimplePOS.Article.RegularArticle getArticleByNumberCore(string number, IObjectContainer db)
        {
            SimplePOS.Article.RegularArticle query = new SimplePOS.Article.RegularArticle(number);
            IObjectSet result = db.QueryByExample(query);
            if (result.Count > 0)
            {
                return (SimplePOS.Article.RegularArticle)result[0];
            }
            else
            {
                return null;
            }
        }

        public SimplePOS.Article.RegularArticle GetArticleByNumber(string number)
        {
            SimplePOS.Article.RegularArticle retval = null;
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    retval = getArticleByNumberCore(number, db);
                }
                finally
                {
                    db.Close();
                }
            }
            return retval;
        }

        public List<SimplePOS.Article.RegularArticle> GetAllArticles()
        {
            List<SimplePOS.Article.RegularArticle> retval = null;
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    retval = new List<SimplePOS.Article.RegularArticle>();
                    SimplePOS.Article.RegularArticle query = new SimplePOS.Article.RegularArticle();
                    IObjectSet result = db.QueryByExample(query);
                    foreach (Object item in result)
                    {
                        retval.Add((SimplePOS.Article.RegularArticle)item);
                    }

                }
                finally
                {
                    db.Close();
                }
            }
            return retval;
        }

        private SimplePOS.Inventory.SaveableStockItem getStockItemByNumberCore(string number, IObjectContainer db)
        {
            SaveableStockItem query = new SaveableStockItem(number, 0);
            IObjectSet result = db.QueryByExample(query);
            if (result.Count > 0)
            {
                return (SaveableStockItem)result[0];
            }
            else
            {
                return null;
            }
        }

        private void deleteStockItemByNumberCore(string number, IObjectContainer db)
        {
            db.Delete(getStockItemByNumberCore(number, db));
        }

        public void DeleteStockItemByNumber(string number)
        {
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    deleteStockItemByNumberCore(number, db);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public void AddItemToStock(SaveableStockItem item)
        {
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    SaveableStockItem old_item = getStockItemByNumberCore(item.Number, db);
                    if (old_item != null)
                    {
                        item.Quantity += old_item.Quantity;
                        deleteStockItemByNumberCore(item.Number, db);
                    }
                    db.Store(item);

                }
                finally
                {
                    db.Close();
                }
            }
        }

        private void setStockCore(SaveableStockItem item, IObjectContainer db)
        {
            SaveableStockItem old_item = getStockItemByNumberCore(item.Number, db);
            if (old_item != null)
            {
                deleteStockItemByNumberCore(item.Number, db);
            }
            db.Store(item);
        }

        public void SetItemToStock(SaveableStockItem item)
        {
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    setStockCore(item, db);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public void TakeOutOfStock(string number, double quantitiy)
        {
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    SaveableStockItem item = getStockItemByNumberCore(number, db);
                    if (item != null)
                    {
                        // Wenn im Lager erfasst
                        item.Quantity -= quantitiy;
                        setStockCore(item, db);
                    }
                }
                finally
                {
                    db.Close();
                }
            }
        }


        public List<SaveableStockItem> GetStock()
        {
            List<SaveableStockItem> retval = null;
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    retval = new List<SaveableStockItem>();
                    SaveableStockItem query = new SaveableStockItem(null, 0);
                    IObjectSet result = db.QueryByExample(query);
                    foreach (Object item in result)
                    {
                        retval.Add((SaveableStockItem)item);
                    }
                }
                finally
                {
                    db.Close();
                }
            }
            return retval;
        }

        public NumberCounter getCounterCore(int year, IObjectContainer db)
        {
            IObjectSet result = db.QueryByExample(new NumberCounter(year));
            if (result.Count > 0)
            {
                return (NumberCounter)result[0];
            }
            else return null;
        }

        public NumberCounter getCounter(int year)
        {
            NumberCounter retval = new NumberCounter(year);
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    IObjectSet result = db.QueryByExample(new NumberCounter(year));
                    if (result.Count > 0)
                    {
                        retval = (NumberCounter)result[0];
                    }
                }
                finally
                {
                    db.Close();
                }
            }
            return retval;
        }

        private void deleteCounterCore(NumberCounter counter, IObjectContainer db)
        {
            db.Delete(counter);
        }

        public void setCounter(NumberCounter counter)
        {
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    NumberCounter old_counter = getCounterCore(counter.Year, db);
                    if (old_counter != null)
                    {
                        deleteCounterCore(old_counter, db);
                    }
                    db.Store(counter);
                }
                finally
                {
                    db.Close();
                }
            }
        }


        public long SaveInvoice(SaveableInvoice invoice)
        {
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    if (invoice != null && invoice.Items.Length > 0)
                    {
                        db.Store(invoice);
                    }
                }
                finally
                {
                    db.Close();
                }
            }
            return invoice.Number;
        }

        public List<SaveableInvoice> GetAllInvoices()
        {
            List<SaveableInvoice> retval = null;
            using (IObjectContainer db = Db4oEmbedded.OpenFile(DB_FILNAME))
            {
                try
                {
                    retval = new List<SaveableInvoice>();

                    retval = new List<SaveableInvoice>();
                    IQuery query = db.Query();
                    query.Constrain(typeof(SaveableInvoice));
                    IObjectSet result = query.Execute();
                    foreach (Object item in result)
                    {
                        retval.Add((SaveableInvoice)item);
                    }
                }
                finally
                {
                    db.Close();
                }
            }
            return retval;
        }

        public void LoadPreferences()
        {
        }
        public void SavePreferences()
        {
        }

        public void ChangeStock(string number, double quantity)
        { 
        
        }

        public double GetStockItemQuantity(string number)
        {
            return 0;
        }

        public int ChangeIventory(string number, double quantity)
        {
            return 0;
        }

        public List<SimplePOS.InventoryItem.InventoryItem> GetInventory()
        {
            return null;
        }

        public void ClearIventory(string number) { }
    }
}
