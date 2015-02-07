// Type: SimplePOS.InvoiceItem
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using System.Text;

namespace SimplePOS
{
  public class InvoiceItem
  {
    private Article article;
    private double quantity;

    public Article Article
    {
      get
      {
        return this.article;
      }
    }

    public double Quantity
    {
      get
      {
        return this.quantity;
      }
      set
      {
        this.quantity = value;
      }
    }

    public InvoiceItem(Article article, double quantity)
    {
      this.article = article;
      this.quantity = quantity;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.article != null)
      {
        stringBuilder.Append(this.article.Number).Append(";");
        stringBuilder.Append(this.article.Name).Append(";");
        stringBuilder.Append(this.quantity).Append(";");
        stringBuilder.Append(this.article.Price).Append(";");
        stringBuilder.Append(this.article.Price * this.quantity).Append(";");
      }
      return ((object) stringBuilder).ToString();
    }
  }
}
