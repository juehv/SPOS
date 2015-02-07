using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePOS.Database
{
    public class Importer
    {
        /// <summary>
        /// Parses a csv file.
        /// http://tech.pro/tutorial/803/csharp-tutorial-using-the-built-in-oledb-csv-parser
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static DataTable ParseCSV(string path)
        {
            if (!File.Exists(path))
                return null;

            string full = Path.GetFullPath(path);
            string file = Path.GetFileName(full);
            string dir = Path.GetDirectoryName(full);

            //create the "database" connection string 
            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;"
              + "Data Source=\"" + dir + "\\\";"
              + "Extended Properties=\"text;HDR=No;FMT=Delimited\"";

            //create the database query
            string query = "SELECT * FROM " + file;

            //create a DataTable to hold the query results
            DataTable dTable = new DataTable();

            //create an OleDbDataAdapter to execute the query
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(query, connString);

            try
            {
                //fill the DataTable
                dAdapter.Fill(dTable);
            }
            catch (InvalidOperationException /*e*/)
            { }

            dAdapter.Dispose();

            return dTable;
        }

        /// <summary>
        /// Imports article from csv file to db. If article with same number exists, it will be replaced.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool ImportArticleFromCsv(ISposDb db, string file)
        {
            // read csv file
            DataTable data = ParseCSV(file);
            if (data == null)
            {
                return false;
            }
            // convert data
            DataTableReader reader = new DataTableReader(data);
            if (reader.FieldCount != 5 && !reader.HasRows)
            {
                return false;
            }
            List<SimplePOS.Article.RegularArticle> articleList = new List<SimplePOS.Article.RegularArticle>();
            reader.Read(); // read the header
            while (reader.Read())
            {
                object[] values = new object[5];
                int result = reader.GetValues(values);
                if (result > 0)
                {
                    try
                    {
                        // Text may be empty wich results in a strange object
                        string text;
                        try { text = (string)values[2]; }
                        catch { text = ""; }
                        articleList.Add(new SimplePOS.Article.RegularArticle((string)values[0],
                            (string)values[1], text,
                            Double.Parse((string)values[3]),
                            Double.Parse((string)values[4])));
                    }
                    catch { return false; }
                }
            }

            // import to db
            db.SaveArticleList(articleList);

            return true;
        }
    }
}
