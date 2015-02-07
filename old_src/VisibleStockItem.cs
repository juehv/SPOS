// Type: SimplePOS.VisibleStockItem
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using SimplePOS.Database;
using System.Collections.Generic;

namespace SimplePOS
{
  public class VisibleStockItem : StockItem
  {
    private string name = "";
    private string text = "";

    public new string Number
    {
      get
      {
        return base.Number;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public new double Quantity
    {
      get
      {
        return base.Quantity;
      }
    }

    public string Text
    {
      get
      {
        return this.text;
      }
    }

    public VisibleStockItem(StockItem item)
      : base(item.Number, item.Quantity)
    {
      Article articleByNumber = DAO_.getInstance().getArticleByNumber(item.Number);
      if (articleByNumber == null)
        return;
      this.name = articleByNumber.Name;
      this.text = articleByNumber.Text;
    }

    public static List<VisibleStockItem> createFromList(List<StockItem> items)
    {
      List<VisibleStockItem> list = new List<VisibleStockItem>();
      foreach (StockItem stockItem in items)
        list.Add(new VisibleStockItem(stockItem));
      return list;
    }
  }
}
