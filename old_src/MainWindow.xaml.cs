// Type: SimplePOS.MainWindow
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using Microsoft.Win32;
using SimplePOS.Database;
using SimplePOS.Invoicing;
using SimplePOS.Preferences;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace SimplePOS
{
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public partial class MainWindow : Window, IComponentConnector
  {
    private Invoice invoice = new Invoice();
    internal ListBox listBox1;
    internal TextBox textBox1;
    internal Button button1;
    internal TextBox textBox2;
    internal Menu menu1;
    internal Button button2;
    internal DatePicker datePicker1;
    internal Button button3;
    internal Label label1;
    internal Label label2;
    internal Label label3;
    private bool _contentLoaded;

    public MainWindow()
    {
      this.InitializeComponent();
      this.clearForm();
    }

    private void stornoAction()
    {
      int selectedIndex = this.listBox1.SelectedIndex;
      if (selectedIndex < 0)
        return;
      InvoiceItem invoiceItem = this.invoice.Items[selectedIndex];
      if (invoiceItem == null || MessageBox.Show("Möchten Sie folgenden Artikel stornieren?\n\"" + invoiceItem.Article.Name + "\"", "Storno?", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
        return;
      this.invoice.Items.Remove(invoiceItem);
      this.listBox1.ItemsSource = (IEnumerable) this.invoice.getClearArticleList();
      this.textBox2.Text = this.invoice.getAmmountAsString();
    }

    private void newInoiceAction()
    {
      if (MessageBox.Show("Möchten Sie die aktuelle Rechnung löschen?", "Löschen?", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
        return;
      this.clearForm();
    }

    private void printInvoiceAction()
    {
      this.invoice.saveToDb();
      if (!InvoicePrinter.getInstance().print(this.invoice))
        return;
      foreach (InvoiceItem invoiceItem in this.invoice.Items)
        DAO_.getInstance().takeOutOfStock(invoiceItem.Article.Number, invoiceItem.Quantity);
      new ReturnCalculator(this.invoice).ShowDialog();
      this.clearForm();
    }

    private void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key != Key.Return)
        return;
      string text = this.textBox1.Text;
      Article articleByNumber = DAO_.getInstance().getArticleByNumber(text);
      if (articleByNumber == null)
      {
        AddArticle addArticle = new AddArticle(new Article(text));
        addArticle.Owner = (Window) this;
        addArticle.ShowDialog();
        articleByNumber = DAO_.getInstance().getArticleByNumber(text);
        if (articleByNumber == null)
          return;
      }
      this.invoice.addItem(articleByNumber);
      this.listBox1.ItemsSource = (IEnumerable) this.invoice.getClearArticleList();
      this.textBox1.Text = "";
      this.textBox2.Text = this.invoice.getAmmountAsString();
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
      AddArticle addArticle = new AddArticle();
      addArticle.Owner = (Window) this;
      addArticle.ShowDialog();
    }

    private void MenuItem_Click_1(object sender, RoutedEventArgs e)
    {
      ArticleManager articleManager = new ArticleManager();
      articleManager.Owner = (Window) this;
      articleManager.ShowDialog();
    }

    private void MenuItem_Click_2(object sender, RoutedEventArgs e)
    {
      StockArticle stockArticle = new StockArticle();
      stockArticle.Owner = (Window) this;
      stockArticle.ShowDialog();
    }

    private void MenuItem_Click_3(object sender, RoutedEventArgs e)
    {
      StockList stockList = new StockList();
      stockList.Owner = (Window) this;
      stockList.ShowDialog();
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      this.printInvoiceAction();
    }

    private void clearForm()
    {
      this.textBox1.Text = "";
      this.textBox2.Text = this.invoice.getAmmountAsString();
      this.invoice = new Invoice();
      this.listBox1.ItemsSource = (IEnumerable) this.invoice.Items;
      this.textBox1.Focus();
      this.datePicker1.SelectedDate = new DateTime?(DateTime.Now);
    }

    private void button2_Click(object sender, RoutedEventArgs e)
    {
      this.newInoiceAction();
    }

    private void MenuItem_Click_5(object sender, RoutedEventArgs e)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "csv files (*.csv)|*.csv";
      bool? nullable = saveFileDialog.ShowDialog();
      if ((!nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0)) == 0)
        return;
      Exporter.INSTANCE.ExportInvoiceToFile(saveFileDialog.FileName);
      int num = (int) MessageBox.Show("History wurde exportiert");
    }

    private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
      this.invoice.Date = this.datePicker1.SelectedDate.Value;
    }

    private void MenuItem_Click_6(object sender, RoutedEventArgs e)
    {
      this.stornoAction();
    }

    private void MenuItem_Click_4(object sender, RoutedEventArgs e)
    {
      int num = (int) MessageBox.Show("Momentan nicht untertützt");
    }

    private void MenuItem_Click_7(object sender, RoutedEventArgs e)
    {
      if (MessageBox.Show("Möchten Sie den letzten Bon erneut drucken?", "Erneuter Druck?", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
        return;
      InvoicePrinter.getInstance().printLastPages();
    }

    private void MenuItem_Click_8(object sender, RoutedEventArgs e)
    {
      this.newInoiceAction();
    }

    private void button3_Click_1(object sender, RoutedEventArgs e)
    {
      this.stornoAction();
    }

    private void MenuItem_Click_9(object sender, RoutedEventArgs e)
    {
      this.printInvoiceAction();
    }

    private void MenuItem_Click_10(object sender, RoutedEventArgs e)
    {
      int num = (int) MessageBox.Show("Momentan nicht untertützt");
    }

    private void MenuItem_Click_11(object sender, RoutedEventArgs e)
    {
      int num = (int) MessageBox.Show("Simple POS\n(c) JUehV-Tech\n\nVersion " + PreferenceManager.VERSION + "\nAchtung: Testversion!\nNicht für den Produktiveinsatz.");
    }

    private void MenuItem_Click_12(object sender, RoutedEventArgs e)
    {
      Process.Start("http://www.juehv-tech.de/spos/versionchecker.php?v=" + PreferenceManager.VERSION);
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/SimplePOS;component/mainwindow.xaml", UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerNonUserCode]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.listBox1 = (ListBox) target;
          break;
        case 2:
          this.textBox1 = (TextBox) target;
          this.textBox1.KeyDown += new KeyEventHandler(this.textBox1_KeyDown);
          break;
        case 3:
          this.button1 = (Button) target;
          this.button1.Click += new RoutedEventHandler(this.button1_Click);
          break;
        case 4:
          this.textBox2 = (TextBox) target;
          break;
        case 5:
          this.menu1 = (Menu) target;
          break;
        case 6:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click_8);
          break;
        case 7:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click_6);
          break;
        case 8:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click_7);
          break;
        case 9:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click_9);
          break;
        case 10:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click);
          break;
        case 11:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click_1);
          break;
        case 12:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click_2);
          break;
        case 13:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click_3);
          break;
        case 14:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click_4);
          break;
        case 15:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click_5);
          break;
        case 16:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click_10);
          break;
        case 17:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click_12);
          break;
        case 18:
          ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_Click_11);
          break;
        case 19:
          this.button2 = (Button) target;
          this.button2.Click += new RoutedEventHandler(this.button2_Click);
          break;
        case 20:
          this.datePicker1 = (DatePicker) target;
          this.datePicker1.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(this.datePicker1_SelectedDateChanged);
          break;
        case 21:
          this.button3 = (Button) target;
          this.button3.Click += new RoutedEventHandler(this.button3_Click_1);
          break;
        case 22:
          this.label1 = (Label) target;
          break;
        case 23:
          this.label2 = (Label) target;
          break;
        case 24:
          this.label3 = (Label) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
