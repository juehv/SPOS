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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimplePOS.Printing
{
    /// <summary>
    /// Interaktionslogik für Page1.xaml
    /// </summary>
    public partial class A4 : Page
    {
        private SimplePOS.Invoicing.Invoice invoice;
        private static int lines = 22;

        public static int LINES { get{return lines;}}

        public A4(SimplePOS.Invoicing.Invoice invoice)
        {
            this.invoice = invoice;
            InitializeComponent();
            listBox1.ItemsSource = invoice.getArticleList();
            listBox2.ItemsSource = invoice.getPriceList();
            textBlock2.Text = string.Format("{0:0.00}", invoice.getAmmount());
            textBlock5.Text = invoice.Date.ToString("dd.MM.yyy");
            textBlock6.Text = invoice.PageNumber;
            if (invoice.ShowTax){
                tax_field.Text = "enthaltene MwSt (" + invoice.TaxSet1 + "%):";
                textBlock3.Text = string.Format("{0:0.00}", invoice.getTax1());
            }
            else
            {
                tax_field.Text = "";
                textBlock3.Text = "Der Betrag enthält keine MwSt gem. §19 UStG";
            }
        }

        public Grid getGrid()
        {
            this.Measure(new System.Windows.Size(592, 555));
            this.Arrange(new System.Windows.Rect(new System.Windows.Point(0, 0), this.DesiredSize));
            return test;
        }
    }
}
