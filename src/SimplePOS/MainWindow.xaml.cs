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
using SimplePOS.Database;
using SimplePOS.Printing;
using Microsoft.Win32;
using System.Net;
using SimplePOS.Article;
using SimplePOS.Invoicing;
using SimplePOS.Stock;
using SimplePOS.Preferences;

namespace SimplePOS
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Invoice invoice = new Invoice();
        private ISposDb db = SQL_DAO.getInstance();

        #region Actions
        private void clearForm()
        {
            invoice = new Invoice();
            textBox1.Text = "";
            textBox2.Text = string.Format("{0:0.00}", invoice.getAmmount());
            listBox1.ItemsSource = invoice.Items;
            textBox1.Focus();
            datePicker1.SelectedDate = DateTime.Now;
            doubleprintMenuItem.IsChecked = Preferences.PreferenceManager.DOUBLEPRINT;
        }

        private void stornoAction()
        {
            int selectedIndex = listBox1.SelectedIndex;
            if (selectedIndex < 0)
            {
                return;
            }
            InvoiceItem item = invoice.Items[selectedIndex];
            if (item == null)
            {
                return;
            }
            MessageBoxResult result = MessageBox.Show("Möchten Sie folgenden Artikel stornieren?\n\""
                + item.Article.Name + "\"", "Storno?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                invoice.Items.Remove(item);
                listBox1.ItemsSource = invoice.getClearArticleList();
                textBox2.Text = string.Format("{0:0.00}", invoice.getAmmount());
            }
        }

        private void newInoiceAction()
        {
            MessageBoxResult result = MessageBox.Show("Möchten Sie die aktuelle Rechnung löschen?",
                "Löschen?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                clearForm();
            }

        }

        private void printInvoiceAction()
        {
            invoice.saveToDb(db);
            if (InvoicePrinter.getInstance().print(invoice))
            {
                foreach (InvoiceItem item in invoice.Items)
                {
                    db.TakeOutOfStock(item.Article.Number, item.Quantity);
                }
                ReturnCalculator cal = new ReturnCalculator(invoice);
                cal.ShowDialog();
                clearForm();
            }
        }
        #endregion

        public MainWindow()
        {
            try
            {
                db.InitDb();
                db.LoadPreferences();
            }
            catch
            {
                MessageBox.Show("Die Datenbank konnten icht initialisiert werden!" +
                    "\nSpos wird daher beendet.\nWenden Sie sich an den Support!",
                    "Schwerer Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(-1);
            } 
            InitializeComponent();
            clearForm();
        }

        // Artikelnummer eingabe
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AbstractArticle article = null;
                string number = textBox1.Text;

                article = ArtikelProcessor.preprocessArtikel(this, db, number);
                if (article == null)
                {
                    return;
                }

                invoice.addItem(article);
                listBox1.ItemsSource = invoice.getClearArticleList();
                textBox1.Text = "";
                textBox2.Text = string.Format("{0:0.00}", invoice.getAmmount());
                string.Format("{0:0.00}", invoice.getAmmount());
            }
        }

        // Artikel - Neu
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ArticleView window = new ArticleView(db);
            window.Owner = this;
            window.ShowDialog();
        }

        // Artikel - Verwalten
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ArticleManager window = new ArticleManager(db);
            window.Owner = this;
            window.ShowDialog();
        }

        // Lager - Einlagern
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Momentan nicht untertützt", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;

            //StockArticle window = new StockArticle(db);
            //window.Owner = this;
            //window.ShowDialog();

        }

        // Lager - Verwalten
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Momentan nicht untertützt", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;

            //StockList window = new StockList(db);
            //window.Owner = this;
            //window.ShowDialog();
        }

        // Drucken Button
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            printInvoiceAction();
        }

        // Neue BUtton
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            newInoiceAction();
        }

        // Extras - Jornal export
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            string path = "";

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "csv files (*.csv)|*.csv";
            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                path = fileDialog.FileName;
                bool exportResult = Exporter.ExportInvoiceJornal(db, path);
                if (exportResult)
                {
                    MessageBox.Show("Jornal wurde exportiert", "Export",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Fehler beim Exportieren", "Export",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            invoice.Date = datePicker1.SelectedDate.Value;
        }

        // Rechnung - Storno
        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            stornoAction();
        }

        // Extras - Inventurmodus
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Momentan nicht untertützt", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        // Rechnung - Beleg 2x drucken
        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            Preferences.PreferenceManager.DOUBLEPRINT = doubleprintMenuItem.IsChecked;
            db.SavePreferences();
        }

        // Rechnung - Neu
        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            newInoiceAction();
        }

        // Sotrno Button
        private void button3_Click_1(object sender, RoutedEventArgs e)
        {
            stornoAction();
        }

        // Rechnung - Drucken
        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            printInvoiceAction();
        }

        // Hilfe - Hilfe aufrufen
        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Momentan nicht untertützt", "", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        // Hilfe - About
        private void MenuItem_Click_11(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Simple POS\n(c) Jens Heuschkel\n\nVersion " + Preferences.PreferenceManager.VERSION +
                "\nAchtung: Testversion!\nNicht für den Produktiveinsatz.", "", MessageBoxButton.OK, MessageBoxImage.None);
        }

        // Hilfe - Auf Updates Prüfen
        private void MenuItem_Click_12(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            string versionString = webClient.DownloadString("http://www.juehv-tech.de/app/spos/update/version.txt");
            if (versionString.Length >= 7)
            {
                string[] version = versionString.Split(new char[] { '.' });
                string[] currVersion = Preferences.PreferenceManager.VERSION.Split(new char[] { '.' });
                bool mustUpdate = false;
                try
                {

                    for (int i = 0; i < 4; i++)
                    {
                        if (Convert.ToInt32(currVersion[i]) > Convert.ToInt32(version[i]))
                        {
                            break;
                        }
                        if (Convert.ToInt32(currVersion[i]) < Convert.ToInt32(version[i]))
                        {
                            mustUpdate = true;
                            break;
                        }
                        //if (Convert.ToInt32(currVersion[i]) == Convert.ToInt32(version[i]))
                        //{
                        //    continue;
                        //}
                    }

                    if (mustUpdate)
                    {
                        MessageBoxResult result = MessageBox.Show("Neuere Version gefunden!\n" +
                            "Möchten Sie die neue Version herunterladen?"
                            , "Update verfügbar", MessageBoxButton.YesNo, MessageBoxImage.Information);
                        if (result == MessageBoxResult.Yes)
                        {
                            System.Diagnostics.Process.Start("http://www.juehv-tech.de/qad/spos/?download");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ihre Software ist aktuell.", "Aktuell", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Fehler beim lesen der Versionsnummer:\n" + versionString, "",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Fehler beim lesen der Versionsnummer:\n" + versionString, "",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Extras - Steuer Einstellungen
        private void MenuItem_Click_13(object sender, RoutedEventArgs e)
        {
            TaxPreferences window = new TaxPreferences(db);
            window.Owner = this;
            window.ShowDialog();
        }
    }
}
