// Type: SimplePOS.Database.DAO_d
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using SimplePOS;
using System.Collections;
using System.Collections.Generic;

namespace SimplePOS.Database
{
  internal class DAO_d
  {
    private static DAO_d INSTANCE = (DAO_d) null;
    private static string DB_FILNAME = "spos.db";

    static DAO_d()
    {
    }

    private DAO_d()
    {
    }

    public static DAO_d getInstance()
    {
      if (DAO_d.INSTANCE == null)
        DAO_d.INSTANCE = new DAO_d();
      return DAO_d.INSTANCE;
    }

    public void saveArticle(Article article)
    {
      using (IObjectContainer db = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          if (this.getArticleByNumberCore(article.Number, db) != null)
            this.deleteArticleByNumberCore(article.Number, db);
          db.Store((object) article);
        }
        finally
        {
          db.Close();
        }
      }
    }

    private void deleteArticleByNumberCore(string number, IObjectContainer db)
    {
      db.Delete((object) this.getArticleByNumberCore(number, db));
    }

    public void deleteArticleByNumber(string number)
    {
      using (IObjectContainer db = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          this.deleteArticleByNumberCore(number, db);
        }
        finally
        {
          db.Close();
        }
      }
    }

    private Article getArticleByNumberCore(string number, IObjectContainer db)
    {
      Article article = new Article(number);
      IObjectSet objectSet = db.QueryByExample((object) article);
      if (objectSet.Count > 0)
        return (Article) objectSet[0];
      else
        return (Article) null;
    }

    public Article getArticleByNumber(string number)
    {
      Article article = (Article) null;
      using (IObjectContainer db = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          article = this.getArticleByNumberCore(number, db);
        }
        finally
        {
          db.Close();
        }
      }
      return article;
    }

    public List<Article> getAllArticles()
    {
      List<Article> list = (List<Article>) null;
      using (IObjectContainer objectContainer = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          list = new List<Article>();
          Article article = new Article();
          foreach (object obj in (IEnumerable) objectContainer.QueryByExample((object) article))
            list.Add((Article) obj);
        }
        finally
        {
          objectContainer.Close();
        }
      }
      return list;
    }

    private StockItem getStockItemByNumberCore(string number, IObjectContainer db)
    {
      StockItem stockItem = new StockItem(number, 0.0);
      IObjectSet objectSet = db.QueryByExample((object) stockItem);
      if (objectSet.Count > 0)
        return (StockItem) objectSet[0];
      else
        return (StockItem) null;
    }

    private void deleteStockItemByNumberCore(string number, IObjectContainer db)
    {
      db.Delete((object) this.getStockItemByNumberCore(number, db));
    }

    public void deleteStockItemByNumber(string number)
    {
      using (IObjectContainer db = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          this.deleteStockItemByNumberCore(number, db);
        }
        finally
        {
          db.Close();
        }
      }
    }

    public void addToStock(StockItem item)
    {
      using (IObjectContainer db = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          StockItem itemByNumberCore = this.getStockItemByNumberCore(item.Number, db);
          if (itemByNumberCore != null)
          {
            item.Quantity += itemByNumberCore.Quantity;
            this.deleteStockItemByNumberCore(item.Number, db);
          }
          db.Store((object) item);
        }
        finally
        {
          db.Close();
        }
      }
    }

    private void setStockCore(StockItem item, IObjectContainer db)
    {
      if (this.getStockItemByNumberCore(item.Number, db) != null)
        this.deleteStockItemByNumberCore(item.Number, db);
      db.Store((object) item);
    }

    public void setStock(StockItem item)
    {
      using (IObjectContainer db = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          this.setStockCore(item, db);
        }
        finally
        {
          db.Close();
        }
      }
    }

    public void takeOutOfStock(string number, double quantitiy)
    {
      using (IObjectContainer db = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          StockItem itemByNumberCore = this.getStockItemByNumberCore(number, db);
          if (itemByNumberCore == null)
            return;
          itemByNumberCore.Quantity -= quantitiy;
          this.setStockCore(itemByNumberCore, db);
        }
        finally
        {
          db.Close();
        }
      }
    }

    public List<StockItem> getStock()
    {
      List<StockItem> list = (List<StockItem>) null;
      using (IObjectContainer objectContainer = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          list = new List<StockItem>();
          StockItem stockItem = new StockItem((string) null, 0.0);
          foreach (object obj in (IEnumerable) objectContainer.QueryByExample((object) stockItem))
            list.Add((StockItem) obj);
        }
        finally
        {
          objectContainer.Close();
        }
      }
      return list;
    }

    public NumberCounter getCounterCore(int year, IObjectContainer db)
    {
      IObjectSet objectSet = db.QueryByExample((object) new NumberCounter(year));
      if (objectSet.Count > 0)
        return (NumberCounter) objectSet[0];
      else
        return (NumberCounter) null;
    }

    public NumberCounter getCounter(int year)
    {
      NumberCounter numberCounter = new NumberCounter(year);
      using (IObjectContainer objectContainer = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          IObjectSet objectSet = objectContainer.QueryByExample((object) new NumberCounter(year));
          if (objectSet.Count > 0)
            numberCounter = (NumberCounter) objectSet[0];
        }
        finally
        {
          objectContainer.Close();
        }
      }
      return numberCounter;
    }

    private void deleteCounterCore(NumberCounter counter, IObjectContainer db)
    {
      db.Delete((object) counter);
    }

    public void setCounter(NumberCounter counter)
    {
      using (IObjectContainer db = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          NumberCounter counterCore = this.getCounterCore(counter.Year, db);
          if (counterCore != null)
            this.deleteCounterCore(counterCore, db);
          db.Store((object) counter);
        }
        finally
        {
          db.Close();
        }
      }
    }

    public void saveInvoice(Invoice invoice)
    {
      using (IObjectContainer objectContainer = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          if (invoice == null || invoice.Items.Count <= 0)
            return;
          objectContainer.Store((object) invoice);
        }
        finally
        {
          objectContainer.Close();
        }
      }
    }

    public List<Invoice> getAllInvoices()
    {
      List<Invoice> list = (List<Invoice>) null;
      using (IObjectContainer objectContainer = (IObjectContainer) Db4oEmbedded.OpenFile(DAO_d.DB_FILNAME))
      {
        try
        {
          list = new List<Invoice>();
          list = new List<Invoice>();
          IQuery query = objectContainer.Query();
          query.Constrain((object) typeof (Invoice));
          foreach (object obj in (IEnumerable) query.Execute())
            list.Add((Invoice) obj);
        }
        finally
        {
          objectContainer.Close();
        }
      }
      return list;
    }
  }
}
