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

namespace SimplePOS.Article
{
    /// <summary>
    /// Interaktionslogik für AddArticle.xaml
    /// </summary>
    public partial class ArticleView : Window
    {
        private ISposDb db;

        public ArticleView(ISposDb db)
        {
            this.db = db;
            InitializeComponent();
            textBox1.Focus();
        }

        public ArticleView(ISposDb db, RegularArticle article)
            : this(db)
        {
            textBox1.Text = article.Number;
            textBox1.IsEnabled = false;
            textBox2.Text = article.Name;
            if (article.Name.Length != 0 && article.Price != 0)
            {
                textBox3.Text = string.Format("{0:0.00}", article.Price);
            }
            textBox4.Text = article.Text;
            double tax = article.Tax;
            if (article.Tax == Preferences.PreferenceManager.TAX_1)
            {
                comboBox1.SelectedIndex = 0;
            }
            else if (article.Tax == Preferences.PreferenceManager.TAX_2)
            {
                comboBox1.SelectedIndex = 1;
            }
            //textBox5.Text = article.Tax.ToString(); translater klasse (enum?)
        }

        // Speichern Button
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Check for article number
            if (textBox1.Text == "")
            {
                MessageBox.Show("Bitte eine Artikel Nr. eingeben.", "",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // build article
            double price = 0;
            double tax = 0;
            try { price = Double.Parse(textBox3.Text); }
            catch
            {
                MessageBox.Show("Fehlerhafte Preisangabe!", "",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (comboBox1.SelectedIndex == 0)
            {
                tax = 19;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                tax = 7;
            }
            string name = textBox2.Text;
            if (name == "")
            {
                name = textBox1.Text;
            }
            RegularArticle article = new RegularArticle
                (textBox1.Text, name, textBox4.Text, price, tax);
            // save to database
            db.SaveArticle(article);

            this.Close();
        }

        private void textBox3_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox3.SelectAll();
        }
    }
}
