// Type: SimplePOS.StockArticle
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
using System.Windows.Input;
using System.Windows.Markup;

namespace SimplePOS
{
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public class StockArticle : Window, IComponentConnector
  {
    private bool singleShow = false;
    internal TextBlock textBlock1;
    internal TextBlock textBlock2;
    internal TextBox textBox1;
    internal TextBox textBox2;
    internal Button button1;
    internal Button button2;
    private bool _contentLoaded;

    public StockArticle()
    {
      this.InitializeComponent();
      this.clearForm();
    }

    public StockArticle(StockItem item)
      : this()
    {
      this.textBox1.Text = item.Number;
      this.textBox2.Text = item.Quantity.ToString();
      this.button2.Visibility = Visibility.Hidden;
      this.singleShow = true;
      this.textBox1.IsEnabled = false;
    }

    private void clearForm()
    {
      this.textBox1.Text = "";
      this.textBox2.Text = "";
      this.textBox1.Focus();
    }

    private void saveItemFromForm()
    {
      string text = this.textBox1.Text;
      if (DAO_.getInstance().getArticleByNumber(text) == null)
      {
        AddArticle addArticle = new AddArticle(new Article(text));
        addArticle.Owner = (Window) this;
        addArticle.ShowDialog();
        if (DAO_.getInstance().getArticleByNumber(text) == null)
          return;
      }
      double quantity;
      try
      {
        quantity = double.Parse(this.textBox2.Text);
      }
      catch
      {
        int num = (int) MessageBox.Show("Bitte Menge eingeben.");
        return;
      }
      StockItem stockItem = new StockItem(text, quantity);
      if (this.singleShow)
        DAO_.getInstance().setStock(stockItem);
      else
        DAO_.getInstance().addToStock(stockItem);
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      this.saveItemFromForm();
      if (this.singleShow)
        this.Close();
      else
        this.clearForm();
    }

    private void textBox2_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key != Key.Return)
        return;
      this.saveItemFromForm();
      if (this.singleShow)
        this.Close();
      else
        this.clearForm();
    }

    private void button2_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    private void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key != Key.Return)
        return;
      this.textBox2.Focus();
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/SimplePOS;component/stock/stockarticle.xaml", UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerNonUserCode]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.textBlock1 = (TextBlock) target;
          break;
        case 2:
          this.textBlock2 = (TextBlock) target;
          break;
        case 3:
          this.textBox1 = (TextBox) target;
          this.textBox1.KeyDown += new KeyEventHandler(this.textBox1_KeyDown);
          break;
        case 4:
          this.textBox2 = (TextBox) target;
          this.textBox2.KeyDown += new KeyEventHandler(this.textBox2_KeyDown);
          break;
        case 5:
          this.button1 = (Button) target;
          this.button1.Click += new RoutedEventHandler(this.button1_Click);
          break;
        case 6:
          this.button2 = (Button) target;
          this.button2.Click += new RoutedEventHandler(this.button2_Click);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
