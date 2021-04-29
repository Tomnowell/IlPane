using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Windows.Storage;


namespace Pane
{

    public static class DataAccess
    {
        const string DBTEMPLATE =
                    "(Primary_Key INTEGER PRIMARY KEY, " +
                    "totalWeight REAL " +
                    "flourWeight REAL" +
                    "waterWeight REAL" +
                    "saltWeight REAL" +
                    "otherDryWeight REAL" +
                    "otherWetWeight REAL" +
                    "bakerPercent REAL" +
                    "ratio REAL" +
                    "saltPercent REAL" +
                    "otherDryPercent REAL" +
                    "totalDryWeight REAL" +
                    "totalWetWeight REAL" +
                    "notes TEXT)";
        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("sqliteSample.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS recipeTable " +
                    DBTEMPLATE;    

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }

        public static void AddData(string inputText)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO recipeTable VALUES (NULL, @Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", inputText);

                insertCommand.ExecuteReader();
                db.Close();
            }

        }


        public static Loaf GetData(int primaryKey)
        {
            List<String> entries = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                selectCommand.CommandText = "SELECT * from recipeTable WHERE Primary_Key = VALUES(@key);");
                selectCommand.Parameters.AddWithValue("@key", primaryKey);

                SqliteDataReader query = selectCommand.ExecuteReader();
                Console.Write(query);
                Loaf currentLoaf = new Loaf();
                while (query.Read())
                {
                    //Here make a Loaf object and copy data from reader.
                    
                    //currentLoaf.TotalWeight = query["totalWeight"];
                    entries.Add(query.GetString(0));
                }

                db.Close();
            }

            return currentLoaf;
        }

    }
}
