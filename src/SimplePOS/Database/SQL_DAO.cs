using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using SimplePOS.Article;
using SimplePOS.Invoicing;
using SimplePOS.Stock;

namespace SimplePOS.Database
{
    //TODO CREATE MUTEX
    /// <summary>
    /// Database access object for sqlite db
    /// </summary>
    public class SQL_DAO : ISposDb
    {
        private static SQL_DAO INSTANCE = null;
        private static string DB_FILNAME = "Data Source=spos.db";

        private System.Threading.Semaphore mutex = new System.Threading.Semaphore(0, 1); //serializes db access
        SQLiteConnection connection;

        private SQL_DAO()
        {
            connection = new SQLiteConnection(DB_FILNAME);
        }

        ~SQL_DAO()
        {
            connection.Dispose();
        }

        public static SQL_DAO getInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new SQL_DAO();
            }
            return INSTANCE;
        }

        public void InitDb()
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);

            // Erstellen der Tabelle, sofern diese noch nicht existiert.
            // Invoice 
            command.CommandText = "CREATE TABLE IF NOT EXISTS invoice ( " +
                "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                "invoice_id INTEGER NOT NULL, " +
                "invoice_date INTEGER NOT NULL, " +
                "invoice_amount REAL NOT NULL," +
                "currency TEXT NOT NULL);";
            command.ExecuteNonQuery();
            // Invoice Article
            command.CommandText = "CREATE TABLE IF NOT EXISTS invoice_item ( " +
                "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                "invoice_id INTEGER NOT NULL, " +
                "item_id TEXT NOT NULL, " +
                "item_name TEXT NOT NULL, " +
                "quantity REAL NOT NULL, " +
                "price REAL NOT NULL, " +
                "sum_price REAL NOT NULL, " +
                "tax REAL NOT NULL);";
            command.ExecuteNonQuery();
            // Artikel
            command.CommandText = "CREATE TABLE IF NOT EXISTS article ( " +
                "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                "item_id TEXT NOT NULL, " +
                "item_name TEXT NOT NULL, " +
                "item_text TEXT, " +
                "price REAL NOT NULL, " +
                "tax REAL NOT NULL);";
            command.ExecuteNonQuery();
            // Lager
            command.CommandText = "CREATE TABLE IF NOT EXISTS stock ( " +
                "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                "item_id TEXT NOT NULL, " +
                "item_quantity REAL NOT NULL);";
            command.ExecuteNonQuery();
            // Einstellungen
            command.CommandText = "CREATE TABLE IF NOT EXISTS preferences ( " +
                "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                "key TEXT NOT NULL, " +
                "value TEXT NOT NULL);";
            command.ExecuteNonQuery();
            // Freigabe der Ressourcen.
            command.Dispose();
            connection.Close();

            // until init, every request will be blocked.
            // but init can be called several times for unblocking so be careful to use init!
            mutex.Release(1);
        }

        public void SaveArticle(SimplePOS.Article.RegularArticle article)
        {
            mutex.WaitOne();
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                //delete old article
                command.CommandText = "DELETE FROM article WHERE item_id LIKE ?;";
                command.Parameters.Add(new SQLiteParameter("item_id", article.Number));
                command.ExecuteNonQuery();

                command.Parameters.Clear();
                command.CommandText = "INSERT INTO article (item_id, item_name, " +
                    "item_text, price, tax) VALUES (?,?,?,?,?);";
                command.Parameters.Add(new SQLiteParameter("item_id", article.Number));
                command.Parameters.Add(new SQLiteParameter("item_name", article.Name));
                command.Parameters.Add(new SQLiteParameter("item_text", article.Text));
                command.Parameters.Add(new SQLiteParameter("price", article.Price));
                command.Parameters.Add(new SQLiteParameter("tax", article.Tax));
                command.ExecuteNonQuery();
            }
            connection.Close();
            mutex.Release();
        }

        public void SaveArticleList(List<SimplePOS.Article.RegularArticle> articleList)
        {
            mutex.WaitOne();
            connection.Open();
            using (SQLiteTransaction transaction = connection.BeginTransaction())
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    // delete old article
                    SQLiteParameter item_id = new SQLiteParameter("item_id");

                    command.CommandText = "DELETE FROM article WHERE item_id LIKE ?;";
                    command.Parameters.Add(item_id);
                    foreach (SimplePOS.Article.RegularArticle article in articleList)
                    {
                        item_id.Value = article.Number;
                        command.ExecuteNonQuery();
                    }

                    // insert new article 
                    SQLiteParameter item_name = new SQLiteParameter("item_name");
                    SQLiteParameter item_text = new SQLiteParameter("item_text");
                    SQLiteParameter price = new SQLiteParameter("price");
                    SQLiteParameter tax = new SQLiteParameter("tax");
                    // Prepare statement
                    command.CommandText = "INSERT INTO article (item_id, item_name, " +
                        "item_text, price, tax) VALUES (?,?,?,?,?);";
                    command.Parameters.Add(item_id);
                    command.Parameters.Add(item_name);
                    command.Parameters.Add(item_text);
                    command.Parameters.Add(price);
                    command.Parameters.Add(tax);
                    // prepare delete statement
                    // execute each statement
                    foreach (SimplePOS.Article.RegularArticle article in articleList)
                    {
                        item_id.Value = article.Number;
                        item_name.Value = article.Name;
                        item_text.Value = article.Text;
                        price.Value = article.Price;
                        tax.Value = article.Tax;
                        command.ExecuteNonQuery();
                    }
                }
                // commit statements
                transaction.Commit();
            }
            connection.Close();
            mutex.Release();
        }

        public void DeleteArticleByNumber(string number)
        {
            mutex.WaitOne();
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM article WHERE item_id LIKE ?;";
                command.Parameters.Add(new SQLiteParameter("item_id", number));
                command.ExecuteNonQuery();
            }
            connection.Close();
            mutex.Release();
        }

        public SimplePOS.Article.RegularArticle GetArticleByNumber(string number)
        {
            mutex.WaitOne();
            SimplePOS.Article.RegularArticle retval = null;
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "SELECT item_id, item_name, item_text, price, tax " +
                    "FROM article WHERE item_id LIKE ? ORDER BY item_id LIMIT 1;";
                command.Parameters.Add(new SQLiteParameter("item_id", number));
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retval = new SimplePOS.Article.RegularArticle(reader[0].ToString(),
                        reader[1].ToString(), reader[2].ToString(), (double)reader[3],
                        (double)reader[4]);
                }
                reader.Close();
            }
            connection.Close();
            mutex.Release();
            return retval;
        }

        public List<SimplePOS.Article.RegularArticle> GetAllArticles()
        {
            mutex.WaitOne();
            List<SimplePOS.Article.RegularArticle> retval = new List<SimplePOS.Article.RegularArticle>();
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {

                command.CommandText = "SELECT item_id, item_name, item_text, price, tax " +
                "FROM article ORDER BY item_id;";
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    retval.Add(new SimplePOS.Article.RegularArticle(reader[0].ToString(),
                        reader[1].ToString(), reader[2].ToString(), (double)reader[3],
                        (double)reader[4]));
                }
                reader.Close();

            }
            connection.Close();
            mutex.Release();
            return retval;
        }

        public void DeleteStockItemByNumber(string number)
        {
            mutex.WaitOne();
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                command.CommandText = "DELETE FROM stock WHERE item_id LIKE ?;";
                command.Parameters.Add(new SQLiteParameter("item_id", number));
                command.ExecuteNonQuery();
            }
            connection.Close();
            mutex.Release();
        }

        public void AddItemToStock(SimplePOS.Stock.SaveableStockItem item)
        {
            mutex.WaitOne();
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                //TODO if exist alter (gemensame funktion
                command.CommandText = "INSERT INTO stock (item_id, item_quantity) VALUES (?,?);";
                command.Parameters.Add(new SQLiteParameter("item_id", item.Number));
                command.Parameters.Add(new SQLiteParameter("item_quantity", item.Quantity));
                command.ExecuteNonQuery();
            }

            connection.Close();
            mutex.Release();
        }

        public void SetItemToStock(SimplePOS.Stock.SaveableStockItem item)
        {
            mutex.WaitOne();
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                //TODO if exist alter
                command.CommandText = "INSERT INTO stock (item_id, item_quantity) VALUES (?,?);";
                command.Parameters.Add(new SQLiteParameter("item_id", item.Number));
                command.Parameters.Add(new SQLiteParameter("item_quantity", item.Quantity));
                command.ExecuteNonQuery();
            }

            connection.Close();
            mutex.Release();
        }

        /// <summary>
        /// Takes the number of quantity articles out of stock.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="quantity"></param>
        public void TakeOutOfStock(string number, double quantity)
        {
            mutex.WaitOne();
            mutex.Release();
        }

        public List<SimplePOS.Stock.SaveableStockItem> GetStock()
        {
            mutex.WaitOne();
            List<SimplePOS.Stock.SaveableStockItem> items = new List<SaveableStockItem>();
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                //request new number
                command.CommandText = "SELECT item_id item_quantity FROM stock ORDER BY item_id ASC;";
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    items.Add(new SaveableStockItem(reader[0].ToString(), (double)reader[1]));
                }
                reader.Close();
            }

            connection.Close();
            mutex.Release();
            return items;
        }

        public long SaveInvoice(SimplePOS.Invoicing.SaveableInvoice invoice)
        {
            mutex.WaitOne();
            long number = -1;
            connection.Open();
            using (SQLiteTransaction transaction = connection.BeginTransaction())
            {
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    //request new number
                    command.CommandText = "SELECT invoice_id FROM invoice WHERE invoice_date > ? ORDER BY invoice_id DESC LIMIT 1;";
                    command.Parameters.Add(new SQLiteParameter("invoice_date",
                        SimplePOS.Util.Timehelper.getTimestampOfDateTime(new DateTime(
                        SimplePOS.Util.Timehelper.getDateTimeOfTimestamp(invoice.Date).Year, 1, 1))));
                    SQLiteDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // got valid number
                        number = (long)reader[0];
                        number++;
                    }
                    else
                    {
                        // no Invoice for this year found
                        number = Preferences.PreferenceManager.INVOICE_NUMBER_START;
                    }
                    invoice.Number = number;
                    reader.Close();

                    // save invoice
                    command.Parameters.Clear();
                    command.CommandText = "INSERT INTO invoice (invoice_id, invoice_date, " +
                        "invoice_amount, currency) VALUES (?, ?, ?, ?);";
                    command.Parameters.Add(new SQLiteParameter("invoice_id", number));
                    command.Parameters.Add(new SQLiteParameter("invoice_date", invoice.Date));
                    command.Parameters.Add(new SQLiteParameter("invoice_amount", invoice.Amount));
                    command.Parameters.Add(new SQLiteParameter("currency", invoice.Currency));
                    command.ExecuteNonQuery();

                    // save items
                    SQLiteParameter invoice_id = new SQLiteParameter("invoice_id");
                    SQLiteParameter item_id = new SQLiteParameter("item_id");
                    SQLiteParameter item_name = new SQLiteParameter("item_name");
                    SQLiteParameter quantity = new SQLiteParameter("quantity");
                    SQLiteParameter price = new SQLiteParameter("price");
                    SQLiteParameter sum_price = new SQLiteParameter("sum_price");
                    SQLiteParameter tax = new SQLiteParameter("tax");
                    command.Parameters.Clear();
                    command.CommandText = "INSERT INTO invoice_item (invoice_id, item_id, " +
                        "item_name, quantity, price, sum_price, tax) VALUES (?,?,?,?,?,?,?);";
                    command.Parameters.Add(invoice_id);
                    command.Parameters.Add(item_id);
                    command.Parameters.Add(item_name);
                    command.Parameters.Add(quantity);
                    command.Parameters.Add(price);
                    command.Parameters.Add(sum_price);
                    command.Parameters.Add(tax);
                    foreach (SaveableInvoiceItem item in invoice.Items)
                    {
                        invoice_id.Value = number;
                        item_id.Value = item.Number;
                        item_name.Value = item.Name;
                        quantity.Value = item.Quantity;
                        price.Value = item.Price;
                        sum_price.Value = item.Sum;
                        tax.Value = item.Tax;
                        command.ExecuteNonQuery();

                    }
                }
                transaction.Commit();
            }
            connection.Close();
            mutex.Release();
            return number;
        }

        public List<SimplePOS.Invoicing.SaveableInvoice> GetAllInvoices()
        {
            mutex.WaitOne();
            List<SimplePOS.Invoicing.SaveableInvoice> invoiceList = new List<SaveableInvoice>();
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                // load invoices
                command.CommandText = "SELECT invoice_id, invoice_date, invoice_amount,currency " +
                "FROM invoice ORDER BY invoice_id;";
                SQLiteDataReader invoiceReader = command.ExecuteReader();

                while (invoiceReader.Read())
                {
                    invoiceList.Add(new SimplePOS.Invoicing.SaveableInvoice((long)invoiceReader[0],
                        (long)invoiceReader[1], (double)invoiceReader[2], invoiceReader[3].ToString()));
                }
                invoiceReader.Close();

                // load items sequentially
                SQLiteParameter invoice_id = new SQLiteParameter("invoice_id");
                command.CommandText = "SELECT item_id, item_name, quantity, price, tax " +
                "FROM invoice_item WHERE invoice_id LIKE ? ORDER BY item_id;";
                command.Parameters.Add(invoice_id);
                foreach (SaveableInvoice invoice in invoiceList)
                {
                    List<SaveableInvoiceItem> itemList = new List<SaveableInvoiceItem>();
                    invoice_id.Value = invoice.Number;
                    SQLiteDataReader itemReader = command.ExecuteReader();

                    while (itemReader.Read())
                    {
                        itemList.Add(new SaveableInvoiceItem(itemReader[0].ToString(), itemReader[1].ToString(),
                            (double)itemReader[2], (double)itemReader[3], (double)itemReader[4]));
                    }
                    itemReader.Close();
                    invoice.Items = itemList.ToArray();
                }

            }
            connection.Close();
            mutex.Release();
            return invoiceList;
        }


        public void SavePreferences()
        {
            mutex.WaitOne();
            // Wenn die preferences stabiler werden, benutzer update
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                //delete old preferences table
                command.CommandText = "DROP TABLE IF EXISTS preferences;";
                command.ExecuteNonQuery();
                // insert new table
                command.CommandText = "CREATE TABLE IF NOT EXISTS preferences ( " +
                    "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                    "key TEXT NOT NULL, " +
                    "value TEXT NOT NULL);";
                command.ExecuteNonQuery();
                // insert preferences
                SQLiteParameter key = new SQLiteParameter("key");
                SQLiteParameter value = new SQLiteParameter("value");
                command.CommandText = "INSERT INTO preferences (key, value) VALUES (?,?);";
                command.Parameters.Add(key);
                command.Parameters.Add(value);

                // doubleprint
                key.Value = "DOUBLEPRINT";
                if (Preferences.PreferenceManager.DOUBLEPRINT)
                {
                    value.Value = "1";
                }
                else
                {
                    value.Value = "0";
                }
                command.ExecuteNonQuery();
                // currency
                key.Value = "CURRENCY";
                value.Value = Preferences.PreferenceManager.CURRENCY_TECH;
                command.ExecuteNonQuery();
                // tax1
                key.Value = "TAX1";
                value.Value = string.Format("{0:0.00}", Preferences.PreferenceManager.TAX_1);
                command.ExecuteNonQuery();
                // tax2
                key.Value = "TAX2";
                value.Value = string.Format("{0:0.00}", Preferences.PreferenceManager.TAX_2);
                command.ExecuteNonQuery();
                // invoice start number
                key.Value = "INVOICE_NUMBER_START";
                value.Value = Preferences.PreferenceManager.INVOICE_NUMBER_START;
                command.ExecuteNonQuery();
                // date format string
                key.Value = "DATE_FORMAT_STRING";
                value.Value = Preferences.PreferenceManager.DATE_FORMAT_STRING;
                command.ExecuteNonQuery();
            }
            connection.Close();
            mutex.Release();
        }

        /// <summary>
        /// Loads the Preferences from the database.
        /// </summary>
        /// <returns></returns>
        public void LoadPreferences()
        {
            mutex.WaitOne();
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                Dictionary<string, string> prefs = new Dictionary<string, string>();
                command.CommandText = "SELECT key, value FROM preferences;";
                SQLiteDataReader reader = command.ExecuteReader();

                // Read pref set
                while (reader.Read())
                {
                    prefs.Add(reader[0].ToString(), reader[1].ToString());
                }
                reader.Close();

                // Write to pref class
                if (prefs.Count == 5)
                {
                    //valid pref set
                    // doubleprint
                    if (prefs["DOUBLEPRINT"].Equals("1"))
                    {
                        Preferences.PreferenceManager.DOUBLEPRINT = true;
                    }
                    else
                    {
                        Preferences.PreferenceManager.DOUBLEPRINT = false;
                    }
                    // currency
                    Preferences.PreferenceManager.CURRENCY_TECH = prefs["CURRENCY"];
                    // tax1
                    Preferences.PreferenceManager.TAX_1 = double.Parse(prefs["TAX1"]);
                    // tax2
                    Preferences.PreferenceManager.TAX_2 = double.Parse(prefs["TAX2"]);
                    // invoice number start
                    Preferences.PreferenceManager.INVOICE_NUMBER_START = long.Parse(prefs["INVOICE_NUMBER_START"]);
                    // date format string
                    Preferences.PreferenceManager.DATE_FORMAT_STRING = prefs["DATE_FORMAT_STRING"];
                }

            }
            connection.Close();
            mutex.Release();
        }
    }
}
