// Type: SimplePOS.A4
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace SimplePOS
{
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public class A4 : Page, IComponentConnector
  {
    private static int lines = 22;
    private Invoice invoice;
    internal Grid test;
    internal Rectangle rectangle1;
    internal ListBox listBox1;
    internal ListBox listBox2;
    internal TextBlock textBlock1;
    internal TextBlock textBlock3;
    internal TextBlock textBlock4;
    internal Label label1;
    internal Label label2;
    internal Label label4;
    internal TextBlock textBlock5;
    internal TextBlock textBlock6;
    internal TextBlock textBlock2;
    private bool _contentLoaded;

    public static int LINES
    {
      get
      {
        return A4.lines;
      }
    }

    static A4()
    {
    }

    public A4(Invoice invoice)
    {
      this.invoice = invoice;
      this.InitializeComponent();
      this.listBox1.ItemsSource = (IEnumerable) invoice.getArticleList();
      this.listBox2.ItemsSource = (IEnumerable) invoice.getPriceList();
      this.textBlock2.Text = invoice.getAmmountAsString();
      this.textBlock3.Text = invoice.getTaxAsString();
      this.textBlock5.Text = invoice.Date.ToString("dd.MM.yyy");
      this.textBlock6.Text = invoice.PageNumber;
    }

    public Grid getGrid()
    {
      this.Measure(new Size(592.0, 555.0));
      this.Arrange(new Rect(new Point(0.0, 0.0), this.DesiredSize));
      return this.test;
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/SimplePOS;component/printing/a4.xaml", UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerNonUserCode]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.test = (Grid) target;
          break;
        case 2:
          this.rectangle1 = (Rectangle) target;
          break;
        case 3:
          this.listBox1 = (ListBox) target;
          break;
        case 4:
          this.listBox2 = (ListBox) target;
          break;
        case 5:
          this.textBlock1 = (TextBlock) target;
          break;
        case 6:
          this.textBlock3 = (TextBlock) target;
          break;
        case 7:
          this.textBlock4 = (TextBlock) target;
          break;
        case 8:
          this.label1 = (Label) target;
          break;
        case 9:
          this.label2 = (Label) target;
          break;
        case 10:
          this.label4 = (Label) target;
          break;
        case 11:
          this.textBlock5 = (TextBlock) target;
          break;
        case 12:
          this.textBlock6 = (TextBlock) target;
          break;
        case 13:
          this.textBlock2 = (TextBlock) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
