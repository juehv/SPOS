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

namespace SimplePOS.Article
{
    /// <summary>
    /// Interaktionslogik für DiscountInputBox.xaml
    /// </summary>
    public partial class DiscountInputBox : Window
    {
        private int returnValue = -1;

        public DiscountInputBox()
        {
            InitializeComponent();
            //TODO load from string db
            this.Title = "Rabatt eingabe";
            line1.Content = "Geben Sie den Rabatt in Cent ein.";
            line2.Content = "Bsp. Eingabe: 1000 -> Rabatt: 10€";
            textBox1.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int input = Int32.Parse(textBox1.Text);
                returnValue = input;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ungültige Eingabe.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, null);
            }
        }

        public int ShowWithResult()
        {
            base.ShowDialog();
            return returnValue;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int input = Int32.Parse(textBox1.Text);
                textBox2.Text = string.Format("{0:0.00}", ((Double)input / 100))
                    + " "
                    + Preferences.PreferenceManager.CURRENCY_FORM;
            }
            catch { textBox2.Text = "kein Rabatt"; }
        }
    }
}
