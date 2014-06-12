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
using Microsoft.Win32;

namespace SimplePOS.Article
{
    /// <summary>
    /// Interaktionslogik für ArticleManager.xaml
    /// </summary>
    public partial class ArticleManager : Window
    {
        private List<RegularArticle> items;
        private ISposDb db;

        public ArticleManager(ISposDb db)
        {
            this.db = db;
            InitializeComponent();
            updateGrid();
        }

        private void updateGrid()
        {
            items = db.GetAllArticles();
            dataGrid1.ItemsSource = items;
        }

        // Neu Button
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ArticleView window = new ArticleView(db);
            window.Owner = this;
            window.ShowDialog();
            updateGrid();
        }

        // Ändern Button
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //TODO model vs view like in java ???
            int index = dataGrid1.SelectedIndex;
            if (index == -1) { return; }

            ArticleView window = new ArticleView(db, items[index]);
            window.Owner = this;
            window.ShowDialog();
            updateGrid();
        }

        // Entfernen Button
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //TODO model vs view like in java ???
            int index = dataGrid1.SelectedIndex;
            if (index == -1) { return; }

            MessageBoxResult result = MessageBox.Show(
                "Möchten Sie den Artikel wirklich entfernen?", "Artikel entfernen?",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                db.DeleteArticleByNumber(items[index].Number);
                updateGrid();
            }
        }

        // Export Button
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "csv files (*.csv)|*.csv";
            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                string path = fileDialog.FileName;
                bool exportResult = Exporter.ExportArticleDb(db, path);
                if (exportResult)
                {
                    MessageBox.Show("Artikel wurden exportiert", "",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Beim Exportieren trat ein Fehler auf", "",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Import Button
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "csv files (*.csv)|*.csv";
            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                string path = fileDialog.FileName;
                bool importResult = Importer.ImportArticleFromCsv(db, path);
                if (importResult)
                {
                    MessageBox.Show("Artikel wurden importiert", "",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Beim Importieren trat ein Fehler auf", "",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            updateGrid();
        }
    }
}
