using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SimplePOS.Database;

namespace SimplePOS.Stock
{
    /// <summary>
    /// Interaktionslogik für StockList.xaml
    /// </summary>
    public partial class StockList : Window
    {
        private List<VisibleStockItem> items;
        private ISposDb db;

        public StockList(ISposDb db)
        {
            this.db = db;
            InitializeComponent();

            updateGrid();
        }

        private void updateGrid()
        {
            items = VisibleStockItem.createFromList(db);
            dataGrid1.ItemsSource = items;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Momentan nicht unterstützt!", "", 
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            StockArticle window = new StockArticle(db);
            window.Owner = this;
            window.ShowDialog();
            updateGrid();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            int index = dataGrid1.SelectedIndex;
            if (index == -1) { return; }

            StockArticle window = new StockArticle(db, items[index]);
            window.Owner = this;
            window.ShowDialog();
            updateGrid();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            int index = dataGrid1.SelectedIndex;
            if (index == -1) { return; }

            MessageBoxResult result = MessageBox.Show(
                "Möchten Sie den Artikel wirklich aus dem Lager entfernen?",
                "Artikel entfernen?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                db.DeleteStockItemByNumber(items[index].Number);
                updateGrid();
            }
        }
    }
}
