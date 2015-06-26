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
    public partial class InventoryItem : Window
    {
        private SimplePOS.Database.ISposDb db;
        public bool stop_inventory = true;

        public InventoryItem(SimplePOS.Database.ISposDb db)
        {
            this.db = db;
            InitializeComponent();
            textBox1.Focus();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            stop_inventory = true;
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            double quantity = 0;
            string number = null;
            int resultcode = 0;

            number = textBox1.Text;
            quantity = Convert.ToDouble(textbox2.Text);

                stop_inventory = false;
                while (stop_inventory == false)
                {
                    try
                    {
                        Convert.ToDouble(textbox2.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Bitte geben Sie als Menge einen gültigen Wert ein.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    //Inventurbestanderfassen wenn nicht initial
                    if (textbox2.Text != null)
                    {
                        resultcode = db.ChangeIventory(number, quantity);
                    }
                    else
                    {
                        MessageBox.Show("Bitte geben Sie eine Artikelnummer ein.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (resultcode != 0)
                    {
                        MessageBox.Show("Artikel ist nicht in Datenbank hinterlegt.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        this.Close();
                        InventoryItem window = new InventoryItem(db);
                        window.ShowDialog();
                    }
                }
        }

        private void textbox2_KeyDown(object sender, KeyEventArgs e)
        {
            double quantity = 0;
            string number = null;
            int resultcode = 0;

            number = textBox1.Text;
       
            if (e.Key == Key.Enter)
            {
                stop_inventory = false;
                while (stop_inventory == false)
                {
                    try
                    {
                        quantity = Convert.ToDouble(textbox2.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Bitte geben Sie als Menge einen gültigen Wert ein.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    //Inventurbestanderfassen wenn nicht initial
                    if (textbox2.Text != null)
                    {
                        resultcode = db.ChangeIventory(number, quantity);
                    }
                    else
                    {
                        MessageBox.Show("Bitte geben Sie eine Artikelnummer ein.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (resultcode != 0)
                    {
                        MessageBox.Show("Artikel ist nicht in Datenbank hinterlegt.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        this.Close();
                        InventoryItem window = new InventoryItem(db);
                        window.ShowDialog();
                    }
                }
                
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            stop_inventory = true;
        }
    }
}
