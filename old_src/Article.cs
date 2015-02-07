// Type: SimplePOS.Article
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

namespace SimplePOS
{
  public class Article
  {
    private string number;
    private string name;
    private double price;
    private double tax;
    private string text;

    public string Number
    {
      get
      {
        return this.number;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public double Price
    {
      get
      {
        return this.price;
      }
    }

    public double Tax
    {
      get
      {
        return this.tax;
      }
    }

    public string Text
    {
      get
      {
        return this.text;
      }
    }

    public Article(string number, string name, string text, double price, double tax)
    {
      this.number = number;
      this.name = name;
      this.text = text;
      this.price = price;
      this.tax = tax;
    }

    public Article(string number)
      : this(number, (string) null, (string) null, 0.0, 0.0)
    {
    }

    public Article()
      : this((string) null)
    {
    }
  }
}
