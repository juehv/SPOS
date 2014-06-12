using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace sqlite_test
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=SqliteTest.db";
            SQLiteConnection con = new SQLiteConnection(connectionString);
            con.Open();
            // requires a table to be created named employee
            // with columns firstname and lastname
            // such as,
            //        CREATE TABLE employee (
            //           firstname varchar(32),
            //           lastname varchar(32));
            SQLiteCommand command = new SQLiteCommand(con);

            // Erstellen der Tabelle, sofern diese noch nicht existiert.
            command.CommandText = "CREATE TABLE IF NOT EXISTS beispiel ( id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name VARCHAR(100) NOT NULL);";
            command.ExecuteNonQuery();

            // Einfügen eines Test-Datensatzes.
            command.CommandText = "INSERT INTO beispiel (id, name) VALUES(NULL, 'Test-Datensatz!')";
            command.ExecuteNonQuery();

            // Freigabe der Ressourcen.
            command.Dispose();

            command = new SQLiteCommand(con);

            // Auslesen des zuletzt eingefügten Datensatzes.
            command.CommandText = "SELECT id, name FROM beispiel ORDER BY id DESC LIMIT 0, 1";

            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("Dies ist der {0}. eingefügte Datensatz mit dem Wert: \"{1}\"", reader[0].ToString(), reader[1].ToString());
            }

            // Beenden des Readers und Freigabe aller Ressourcen.
            reader.Close();
            reader.Dispose();

            command.Dispose();

            con.Close();
            con.Dispose();

            Console.ReadLine();
            
        }
    }
}
