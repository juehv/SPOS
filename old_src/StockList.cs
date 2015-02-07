// Type: SimplePOS.StockList
// Assembly: SimplePOS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F961FDEE-768E-48B5-A7C6-85289227D7E1
// Assembly location: C:\Users\Jens\AppData\Local\Apps\2.0\5P84V0ME.K06\0XB3W4RN.YQR\simp..tion_4e70cf8437ccfc01_0000.0001_905aba644949de0b\SimplePOS.exe

using SimplePOS.Database;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SimplePOS
{
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public class StockList : Window, IComponentConnector
  {
    private List<VisibleStockItem> items;
    internal DataGrid dataGrid1;
    internal Button button1;
    internal Button button2;
    internal Button button3;
    internal Button button4;
    private bool _contentLoaded;

    public StockList()
    {
      this.InitializeComponent();
      this.items = VisibleStockItem.createFromList(DAO_.getInstance().getStock());
      this.dataGrid1.ItemsSource = (IEnumerable) this.items;
    }

    private void updateGrid()
    {
      this.items = VisibleStockItem.createFromList(DAO_.getInstance().getStock());
      this.dataGrid1.ItemsSource = (IEnumerable) this.items;
    }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
      int num = (int) MessageBox.Show("Momentan nicht unterstützt!");
    }

    private void button2_Click(object sender, RoutedEventArgs e)
    {
      StockArticle stockArticle = new StockArticle();
      stockArticle.Owner = (Window) this;
      stockArticle.ShowDialog();
      this.updateGrid();
    }

    private void button3_Click(object sender, RoutedEventArgs e)
    {
      int selectedIndex = this.dataGrid1.SelectedIndex;
      if (selectedIndex == -1)
        return;
      StockArticle stockArticle = new StockArticle((StockItem) this.items[selectedIndex]);
      stockArticle.Owner = (Window) this;
      stockArticle.ShowDialog();
      this.updateGrid();
    }

    private void button4_Click(object sender, RoutedEventArgs e)
    {
      int selectedIndex = this.dataGrid1.SelectedIndex;
      if (selectedIndex == -1 || MessageBox.Show("Möchten Sie den Artikel wirklich aus dem Lager entfernen?", "Artikel entfernen?", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
        return;
      DAO_.getInstance().deleteStockItemByNumber(this.items[selectedIndex].Number);
      this.updateGrid();
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/SimplePOS;component/stock/stocklist.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.dataGrid1 = (DataGrid) target;
          break;
        case 2:
          this.button1 = (Button) target;
          this.button1.Click += new RoutedEventHandler(this.button1_Click);
          break;
        case 3:
          this.button2 = (Button) target;
          this.button2.Click += new RoutedEventHandler(this.button2_Click);
          break;
        case 4:
          this.button3 = (Button) target;
          this.button3.Click += new RoutedEventHandler(this.button3_Click);
          break;
        case 5:
          this.button4 = (Button) target;
          this.button4.Click += new RoutedEventHandler(this.button4_Click);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
