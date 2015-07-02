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
    /// Interaktionslogik für StoreArticle.xaml
    /// </summary>
    public partial class StockArticle : Window
    {
        private bool singleShow = false;
        private ISposDb db;
        private double curr_quantity;
        public bool stop_stocking = true;
        

        public StockArticle(ISposDb db)
        {
            this.db = db;
            InitializeComponent();
            clearForm();
        }

        public StockArticle(ISposDb db,SaveableStockItem item)
            : this(db)
        {
            textBox1.Text = item.Number;
            textBox2.Text = item.Quantity.ToString();
            curr_quantity = item.Quantity;
            button2.Visibility = Visibility.Hidden;
            singleShow = true;
            textBox1.IsEnabled=false;
        }

        private void clearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
        }

        private void saveItemFromForm()
        {
            string number = textBox1.Text;
            SimplePOS.Article.AbstractArticle article = db.GetArticleByNumber(number);
            if (article == null)
            {
                // Artikel nicht vorhanden
                SimplePOS.Article.ArticleView window = new 
                    SimplePOS.Article.ArticleView(db, 
                    new SimplePOS.Article.RegularArticle(number));
                window.Owner = this;
                window.ShowDialog();
                article = db.GetArticleByNumber(number);
                // wenn nichts gültiges eingegeben wird --> beenden
                if (article == null)
                {
                    return;
                }
                
            }


            double quantity = 0;
            try { quantity = Double.Parse(textBox2.Text); }
            catch
            {
                MessageBox.Show("Bitte Menge eingeben.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SaveableStockItem item = new SaveableStockItem(number, quantity);
            if (singleShow)
            {
                curr_quantity += item.Quantity;
                item.Quantity = curr_quantity;
                db.SetItemToStock(item);
            }
            else
            {
                db.AddItemToStock(item);
            }
            stop_stocking = false;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            saveItemFromForm();
            if (singleShow)
            {
                this.Close();
            }
            else
            {
                clearForm();
            }

            stop_stocking = false;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                saveItemFromForm();
                if (singleShow)
                {
                    this.Close();
                }
                else
                {
                    clearForm();
                }
            }
            stop_stocking = false;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBox2.Focus();
            }
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                stop_stocking = true;
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }




    }
}
