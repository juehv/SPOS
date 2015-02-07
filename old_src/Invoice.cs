// Type: SimplePOS.Invoice
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using SimplePOS.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePOS
{
  public class Invoice
  {
    private static string CURRENCY = " €";
    private string pageNumber = (string) null;
    private DateTime date;
    private long number;
    private List<InvoiceItem> items;

    public string PageNumber
    {
      get
      {
        if (this.pageNumber == null || this.pageNumber == "")
          return this.number.ToString();
        else
          return this.pageNumber;
      }
      set
      {
        this.pageNumber = value;
      }
    }

    public DateTime Date
    {
      get
      {
        return new DateTime(this.date.Ticks);
      }
      set
      {
        this.date = value;
      }
    }

    public List<InvoiceItem> Items
    {
      get
      {
        return this.items;
      }
    }

    public long Number
    {
      get
      {
        return this.number;
      }
    }

    static Invoice()
    {
    }

    public Invoice()
      : this(0L, DateTime.Now, new List<InvoiceItem>())
    {
    }

    public Invoice(long number)
      : this(number, DateTime.Now, (List<InvoiceItem>) null)
    {
    }

    public Invoice(Invoice parent, int page, List<InvoiceItem> items)
      : this(parent.Number, parent.Date, items)
    {
      this.pageNumber = (string) (object) this.number + (object) "-" + (string) (object) page;
    }

    public Invoice(long number, DateTime date, List<InvoiceItem> items)
    {
      this.number = number;
      this.date = date;
      this.items = items;
    }

    public void saveToDb()
    {
      this.number = NumberManager.getInstance().getNextNumberOfCurrentYear();
      DAO_.getInstance().saveInvoice(new SaveableInvoice(this));
    }

    private void addItem(Article article, double quantity)
    {
      foreach (InvoiceItem invoiceItem in this.items)
      {
        if (invoiceItem.Article.Number == article.Number)
        {
          ++invoiceItem.Quantity;
          return;
        }
      }
      this.items.Add(new InvoiceItem(article, quantity));
    }

    public void addItem(Article article)
    {
      this.addItem(article, 1.0);
    }

    public void takeNumberFromDb()
    {
    }

    public List<string> getArticleList()
    {
      List<string> list = new List<string>();
      foreach (InvoiceItem invoiceItem in this.items)
      {
        list.Add(invoiceItem.Article.Name);
        if (invoiceItem.Quantity > 1.0)
          list.Add("    " + string.Format("{0:00}", (object) invoiceItem.Quantity) + " x " + string.Format("{0:0.00}", (object) invoiceItem.Article.Price) + Invoice.CURRENCY);
      }
      return list;
    }

    public List<string> getClearArticleList()
    {
      List<string> list = new List<string>();
      foreach (InvoiceItem invoiceItem in this.items)
        list.Add(string.Format("{0:00}", (object) invoiceItem.Quantity) + " x " + invoiceItem.Article.Name);
      return list;
    }

    public List<string> getPriceList()
    {
      List<string> list = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (InvoiceItem invoiceItem in this.items)
      {
        if (invoiceItem.Quantity > 1.0)
          list.Add(" ");
        stringBuilder.Clear();
        list.Add(((object) stringBuilder.AppendFormat("{0:0.00}", (object) (invoiceItem.Article.Price * invoiceItem.Quantity)).Append(Invoice.CURRENCY)).ToString());
      }
      return list;
    }

    public double getAmmount()
    {
      double num = 0.0;
      foreach (InvoiceItem invoiceItem in this.items)
        num += invoiceItem.Quantity * invoiceItem.Article.Price;
      return num;
    }

    public string getAmmountAsString()
    {
      return string.Format("{0:0.00}", (object) this.getAmmount()) + Invoice.CURRENCY;
    }

    public double getTax()
    {
      double num1 = 0.0;
      foreach (InvoiceItem invoiceItem in this.items)
      {
        double num2 = invoiceItem.Article.Price / 1.19;
        double num3 = invoiceItem.Article.Price - num2;
        num1 += invoiceItem.Quantity * num3;
      }
      return num1;
    }

    public string getTaxAsString()
    {
      return string.Format("{0:0.00}", (object) this.getTax()) + Invoice.CURRENCY;
    }

    public double calculateReturn(double money)
    {
      return money - this.getAmmount();
    }

    public string getReturnAsString(double money)
    {
      return ((object) new StringBuilder().AppendFormat("{0:0.00}", (object) this.calculateReturn(money)).Append(Invoice.CURRENCY)).ToString();
    }

    public Invoice Clone()
    {
      return new Invoice(this.number, this.date, this.items);
    }
  }
}
