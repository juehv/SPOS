using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimplePOS.Inventory
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class InventoryOverview : Window
    {
        private List<SimplePOS.InventoryItem.InventoryItem> InventoryList;
        private SimplePOS.Database.ISposDb db;
        public InventoryOverview(SimplePOS.Database.ISposDb db)
        {
            this.db = db;
            InitializeComponent();
            updategrid();
        }

        private void updategrid()
        {
            InventoryList = db.GetInventory();
            DataGrid1.ItemsSource = InventoryList;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (SimplePOS.InventoryItem.InventoryItem selectditem in DataGrid1.SelectedItems)
            {
                db.ChangeStock(selectditem.number, selectditem.actual_stock);
                db.ClearIventory(selectditem.number);
                
            }
            updategrid();
        }
    }
}
