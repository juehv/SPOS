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

namespace SimplePOS.Inventory
{
    /// <summary>
    /// Interaktionslogik für StockList.xaml
    /// </summary>
    public partial class StockList : Window
    {
        private List<VisibleStockItem> items;
        private ISposDb db;
        private List<SaveableStockItem> tmpitems = new List<SaveableStockItem>();

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
            //MessageBox.Show("Momentan nicht unterstützt!", "", 
            //    MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            bool stop_stocking = false;
            int index = dataGrid1.SelectedIndex;
            if (index == -1) { return; }

            while (stop_stocking == false)
            {
                
                try 
                {
                    StockArticle window = new StockArticle(db, items[index]);
                    window.Owner = this;
                    window.Focusable = true;
                    window.textBlock2.Focusable = true;
                    window.textBox2.Focus();
                    Keyboard.Focus(window.textBox2);
                    window.ShowDialog();
                    updateGrid();
                    ++index;
                    stop_stocking = window.stop_stocking; 

                }
                catch (Exception)
                {
                    stop_stocking = true;
                }

            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.IsReadOnly = false;

            //Setzen der Read-Only Eigenschaft --> darf nich global gestetzt werden
            //da sonst nicht verändert werden darf
            foreach (var colums in dataGrid1.Columns)
            {

                colums.IsReadOnly = true;

            }

            //Kopie der List vor Änderungen eingeben
            foreach (VisibleStockItem item in items)
            {
                tmpitems.Add(new SaveableStockItem(item.Number, item.Quantity));
            }

            dataGrid1.Columns[2].IsReadOnly = false;
            button10.IsEnabled = true;
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

        private void button10_Click(object sender, RoutedEventArgs e)
        {

            foreach (VisibleStockItem item in items)
            {

                foreach (SaveableStockItem tmpitem in tmpitems)
                {

                    if (item.Number == tmpitem.Number && item.Quantity != tmpitem.Quantity)
                    {

                        db.ChangeStock(item.Number, item.Quantity);

                    }

                }

            }

            dataGrid1.Columns[2].IsReadOnly = true;
            button10.IsEnabled = false;
        }

        
    }
}
