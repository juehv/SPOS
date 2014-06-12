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

namespace SimplePOS.Invoicing
{
    /// <summary>
    /// Interaktionslogik für ReturnCalculator.xaml
    /// </summary>
    public partial class ReturnCalculator : Window
    {
        private Invoice invoice;
        public ReturnCalculator(Invoice invoice)
        {
            this.invoice = invoice;
            InitializeComponent();
            textBox1.Text = string.Format("{0:0.00}", invoice.getAmmount());
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                double money = double.Parse(textBox2.Text);
                textBox3.Text = string.Format("{0:0.00}", invoice.calculateReturn(money));
            }
            catch { textBox3.Text = ""; }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
