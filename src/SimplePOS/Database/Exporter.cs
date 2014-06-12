using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace SimplePOS.Database
{
    /// <summary>
    /// Exports db data to csv files. Static class because there is no internal status needed.
    /// </summary>
    public class Exporter
    {
        #region Constants
        public const string JORNAL_HEAD = "Beleg-Nr.,Datum,Rechnungsbetrag,Waehrung," +
            "Artikel-Nr.,Artikel-Beschreibung,Artikel-Menge,Artikel-Einzelpreis," +
            "Artikel-Gesamtpreis,Steuersatz";
        public const string JORNAL_EMPTY_LINE = ",,,,,,,,,";

        public const string ARTICLE_HEAD = "Artikel-Nr.,Name,Beschreibung,Preis," +
            "Steuersatz";
        #endregion

        private Exporter() {/*empty*/}

        #region Methods
        public static bool ExportInvoiceJornal(ISposDb db, string filepath)
        {
            try
            {
                // Datei öffnen
                TextWriter writer = new StreamWriter(filepath);
                // Rechnungen aus der Datenbank laden.
                List<SimplePOS.Invoicing.SaveableInvoice> invoices = db.GetAllInvoices();
                // Header schreiben
                writer.WriteLine(JORNAL_HEAD);
                // Daten schreiben
                foreach (SimplePOS.Invoicing.SaveableInvoice invoice in invoices)
                {
                    writer.WriteLine(invoice.ToCsvLines());
                    writer.WriteLine(JORNAL_EMPTY_LINE);
                }
                // Datei schließen
                writer.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ExportArticleDb(ISposDb db, string filepath)
        {
            try
            {
                // Datei öffnen
                TextWriter writer = new StreamWriter(filepath);
                // Rechnungen aus der Datenbank laden.
                List<SimplePOS.Article.RegularArticle> articles = db.GetAllArticles();
                // Header schreiben
                writer.WriteLine(ARTICLE_HEAD);
                // Daten schreiben
                foreach (SimplePOS.Article.RegularArticle article in articles)
                {
                    writer.WriteLine(article.ToCsvLine());
                }
                // Datei schließen
                writer.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
