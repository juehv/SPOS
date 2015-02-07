// Type: SimplePOS.SaveableInvoice
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using System;
using System.Collections.Generic;

namespace SimplePOS
{
  public class SaveableInvoice
  {
    private List<string> items = new List<string>();
    private DateTime date;
    private long number;
    private double amount;

    public DateTime Date
    {
      get
      {
        return this.date;
      }
    }

    public long Number
    {
      get
      {
        return this.number;
      }
    }

    public List<string> Items
    {
      get
      {
        return this.items;
      }
    }

    public double Amount
    {
      get
      {
        return this.amount;
      }
    }

    public string AmountString
    {
      get
      {
        return string.Format("{0:c}", (object) this.amount);
      }
    }

    public SaveableInvoice(Invoice invoice)
    {
      this.number = invoice.Number;
      this.date = invoice.Date;
      foreach (object obj in invoice.Items)
        this.items.Add(obj.ToString());
      this.amount = invoice.getAmmount();
    }
  }
}
