// Type: SimplePOS.InvoicePrinter
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using SimplePOS.Preferences;
using SimplePOS.Printing;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimplePOS
{
  public class InvoicePrinter
  {
    private static InvoicePrinter INSTANCE = (InvoicePrinter) null;
    private List<A4> lastPages = (List<A4>) null;

    static InvoicePrinter()
    {
    }

    private InvoicePrinter()
    {
    }

    public static InvoicePrinter getInstance()
    {
      if (InvoicePrinter.INSTANCE == null)
        InvoicePrinter.INSTANCE = new InvoicePrinter();
      return InvoicePrinter.INSTANCE;
    }

    public bool print(Invoice items)
    {
      foreach (InvoiceItem invoiceItem in items.Items)
        Console.WriteLine(invoiceItem.Article.Name + (object) " x" + (string) (object) invoiceItem.Quantity);
      PrintDialog printDialog = new PrintDialog();
      bool? nullable = printDialog.ShowDialog();
      if ((!nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0)) == 0)
        return false;
      List<Invoice> list = PrintingPreprocessor.ProcessInvoices(items);
      this.lastPages = new List<A4>();
      foreach (Invoice invoice in list)
      {
        A4 a4 = new A4(invoice);
        a4.Measure(new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));
        a4.Arrange(new Rect(new Point(0.0, 0.0), a4.DesiredSize));
        this.lastPages.Add(a4);
        printDialog.PrintVisual((Visual) a4.getGrid(), invoice.PageNumber);
        if (PreferenceManager.DOUBLEPRINT)
          printDialog.PrintVisual((Visual) a4.getGrid(), invoice.PageNumber);
      }
      return true;
    }

    public bool printLastPages()
    {
      if (this.lastPages == null)
        return false;
      PrintDialog printDialog = new PrintDialog();
      bool? nullable = printDialog.ShowDialog();
      if ((!nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0)) == 0)
        return false;
      foreach (A4 a4 in this.lastPages)
        printDialog.PrintVisual((Visual) a4.getGrid(), "reprint job");
      return true;
    }
  }
}
