// Type: SimplePOS.AddArticle
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using SimplePOS.Database;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SimplePOS
{
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public class AddArticle : Window, IComponentConnector
  {
    internal TextBox textBox1;
    internal TextBlock textBlock1;
    internal TextBlock textBlock2;
    internal TextBlock textBlock3;
    internal TextBlock textBlock4;
    internal TextBox textBox2;
    internal TextBox textBox3;
    internal TextBox textBox4;
    internal Button button1;
    internal TextBlock textBlock5;
    internal TextBlock textBlock6;
    internal ComboBox comboBox1;
    private bool _contentLoaded;

    public AddArticle()
    {
      this.InitializeComponent();
      this.textBox1.Focus();
    }

    public AddArticle(Article article)
      : this()
    {
      this.textBox1.Text = article.Number;
      this.textBox1.IsEnabled = false;
      this.textBox2.Text = article.Name;
      this.textBox3.Text = string.Format("{0:0.00}", (object) article.Price);
      this.textBox4.Text = article.Text;
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      if (this.textBox1.Text == "")
      {
        int num1 = (int) MessageBox.Show("Bitte eine Artikel Nr. eingeben.");
      }
      else
      {
        double tax = 0.0;
        double price;
        try
        {
          price = double.Parse(this.textBox3.Text);
        }
        catch
        {
          int num2 = (int) MessageBox.Show("Fehlerhafte Preisangabe!");
          return;
        }
        try
        {
          tax = double.Parse("19");
        }
        catch
        {
        }
        string text = this.textBox2.Text;
        if (text == "")
          text = this.textBox1.Text;
        DAO_.getInstance().saveArticle(new Article(this.textBox1.Text, text, this.textBox4.Text, price, tax));
        this.Close();
      }
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/SimplePOS;component/article/addarticle.xaml", UriKind.Relative));
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
          this.textBlock1 = (TextBlock) target;
          break;
        case 3:
          this.textBlock2 = (TextBlock) target;
          break;
        case 4:
          this.textBlock3 = (TextBlock) target;
          break;
        case 5:
          this.textBlock4 = (TextBlock) target;
          break;
        case 6:
          this.textBox2 = (TextBox) target;
          break;
        case 7:
          this.textBox3 = (TextBox) target;
          break;
        case 8:
          this.textBox4 = (TextBox) target;
          break;
        case 9:
          this.button1 = (Button) target;
          this.button1.Click += new RoutedEventHandler(this.button1_Click);
          break;
        case 10:
          this.textBlock5 = (TextBlock) target;
          break;
        case 11:
          this.textBlock6 = (TextBlock) target;
          break;
        case 12:
          this.comboBox1 = (ComboBox) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
