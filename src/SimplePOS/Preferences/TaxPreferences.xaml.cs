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

namespace SimplePOS.Preferences
{
    /// <summary>
    /// Interaktionslogik für TaxPreferences.xaml
    /// </summary>
    public partial class TaxPreferences : Window
    {
        private Database.ISposDb db;


        public TaxPreferences(Database.ISposDb db)
        {
            this.db = db;
            InitializeComponent();
            showTaxCheckbox.IsChecked = PreferenceManager.SHOW_TAX;
            tax1Textbox.Text = PreferenceManager.TAX_1.ToString();
            tax2Textbox.Text = PreferenceManager.TAX_2.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // TODO input validation
            PreferenceManager.SHOW_TAX = (showTaxCheckbox.IsChecked.HasValue) ? (bool) showTaxCheckbox.IsChecked : true;
            PreferenceManager.TAX_1 = (tax1Textbox.Text.Length != 0) ? Double.Parse(tax1Textbox.Text) : 0;
            PreferenceManager.TAX_2 = (tax2Textbox.Text.Length != 0) ? Double.Parse(tax2Textbox.Text) : 0;
            db.SavePreferences();
            this.Close();
        }

        private void showTaxCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)!showTaxCheckbox.IsChecked)
            {
                tax1Textbox.Text = "0";
                tax2Textbox.Text = "0";
                tax1Textbox.IsEnabled = false;
                tax2Textbox.IsEnabled = false;
            }
            else
            {
                tax1Textbox.Text = PreferenceManager.STANDARD_TAX_1.ToString();
                tax2Textbox.Text = PreferenceManager.STANDARD_TAX_2.ToString();
                tax1Textbox.IsEnabled = true;
                tax2Textbox.IsEnabled = true;
            }
        }
    }
}
