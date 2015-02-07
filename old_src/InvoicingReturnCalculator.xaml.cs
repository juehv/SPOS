// Type: SimplePOS.Invoicing.ReturnCalculator
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using SimplePOS;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace SimplePOS.Invoicing
{
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public partial class ReturnCalculator : Window, IComponentConnector
  {
    private Invoice invoice;
    internal TextBox textBox1;
    internal TextBox textBox2;
    internal Button button1;
    internal Label label1;
    internal Label label2;
    internal TextBox textBox3;
    internal Label label3;
    private bool _contentLoaded;

    public ReturnCalculator(Invoice invoice)
    {
      this.invoice = invoice;
      this.InitializeComponent();
      this.textBox1.Text = invoice.getAmmountAsString();
    }

    private void textBox2_KeyUp(object sender, KeyEventArgs e)
    {
      try
      {
        this.textBox3.Text = this.invoice.getReturnAsString(double.Parse(this.textBox2.Text));
      }
      catch
      {
        this.textBox3.Text = "";
      }
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/SimplePOS;component/invoicing/returncalculator.xaml", UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerNonUserCode]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.textBox1 = (TextBox) target;
          break;
        case 2:
          this.textBox2 = (TextBox) target;
          this.textBox2.KeyUp += new KeyEventHandler(this.textBox2_KeyUp);
          break;
        case 3:
          this.button1 = (Button) target;
          this.button1.Click += new RoutedEventHandler(this.button1_Click);
          break;
        case 4:
          this.label1 = (Label) target;
          break;
        case 5:
          this.label2 = (Label) target;
          break;
        case 6:
          this.textBox3 = (TextBox) target;
          break;
        case 7:
          this.label3 = (Label) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
