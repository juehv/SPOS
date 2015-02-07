// Type: SimplePOS.Printing.PrintingPreprocessor
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using SimplePOS;
using System.Collections.Generic;

namespace SimplePOS.Printing
{
  internal class PrintingPreprocessor
  {
    public static int CheckLinesLeft(List<string> list)
    {
      return A4.LINES - list.Count;
    }

    private static bool fitsInPage(int lines)
    {
      return A4.LINES >= lines;
    }

    public static List<Invoice> ProcessInvoices(Invoice invoice)
    {
      List<Invoice> list1 = new List<Invoice>();
      int lines1 = 0;
      foreach (InvoiceItem invoiceItem in invoice.Items)
      {
        ++lines1;
        if (invoiceItem.Quantity > 1.0)
          ++lines1;
      }
      if (!PrintingPreprocessor.fitsInPage(lines1))
      {
        int lines2 = 0;
        int num1 = 1;
        List<InvoiceItem> items1 = new List<InvoiceItem>();
        foreach (InvoiceItem invoiceItem in invoice.Items)
        {
          ++lines2;
          if (invoiceItem.Quantity > 1.0)
            ++lines2;
          if (PrintingPreprocessor.fitsInPage(lines2))
          {
            items1.Add(invoiceItem);
          }
          else
          {
            lines2 = 0;
            list1.Add(new Invoice(invoice, num1++, items1));
            items1 = new List<InvoiceItem>();
            items1.Add(invoiceItem);
          }
        }
        if (items1.Count > 0)
        {
          List<Invoice> list2 = list1;
          Invoice parent = invoice;
          int page = num1;
          int num2 = 1;
          int num3 = page + num2;
          List<InvoiceItem> items2 = items1;
          Invoice invoice1 = new Invoice(parent, page, items2);
          list2.Add(invoice1);
        }
      }
      else
        list1.Add(invoice);
      return list1;
    }
  }
}
