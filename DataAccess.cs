using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Data.Sqlite;
using Windows.Storage;


namespace Pane
{

    public static class DataAccess
    {
        const string DBTEMPLATE = "(\"Key\"	INTEGER NOT NULL UNIQUE," +
            "\"Name\"	            TEXT NOT NULL UNIQUE," +
            "\"TotalWeight\"	    REAL," +
            "\"FlourWeight\"	    REAL," +
            "\"WaterWeight\"	    REAL," +
            "\"SaltWeight\"	        REAL," +
            "\"OtherDryWeight\"	    REAL," +
            "\"OtherWetWeight\"	    REAL," +
            "\"Ratio\"	            REAL," +
            "\"SaltPercent\"	    REAL," +
            "\"OtherDryPercent\"	REAL," +
            "\"TotalDryWeight\"	    REAL," +
            "\"TotalWetWeight\"	    REAL," +
            "\"Notes\"	            TEXT," +
            "PRIMARY KEY(\"key\"    AUTOINCREMENT));";

        public async static void InitializeDatabase()
        {
            Console.WriteLine("initializing database...");
            await ApplicationData.Current.LocalFolder.CreateFileAsync("BreadRecipes.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "BreadRecipes.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}")) 
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT EXISTS \"recipeTable\"" + DBTEMPLATE;    

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
           
        }
        public static void DeleteData(Loaf currentLoaf)
        {
            // Deletes the database entry of the selected item in ListView
            if (currentLoaf == null || currentLoaf.RecipeName == "")
            {
                //No loaf to delete display error dialogue
                throw new SqliteException("Error", 1);
            }
            else
            {
                string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "BreadRecipes.db");
                using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
                {
                    db.Open();
                    SqliteCommand deleteCommand = new SqliteCommand();
                    deleteCommand.Connection = db;

                    // Use parameterized query to prevent SQL injection attacks
                    deleteCommand.CommandText = "DELETE FROM recipeTable WHERE Name = @Name;";
                    deleteCommand.Parameters.AddWithValue("@Name", currentLoaf.RecipeName);
                    deleteCommand.ExecuteReader();
                    db.Close();
                }
            }
        }

        public static void OverwriteData(Loaf currentLoaf)
        {
                DeleteData(currentLoaf);
                AddData(currentLoaf);
        }
        public static void AddData(Loaf currentLoaf)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "BreadRecipes.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                // Use parameterized query to prevent SQL injection attacks
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO recipeTable VALUES (NULL,@Name,"+
                    "@TotalWeight, @FlourWeight, @WaterWeight, @SaltWeight, " +
                    "@OtherDryWeight, @OtherWetWeight, @Ratio, @BakerPercent, " +
                    "@SaltPercent, @OtherDryPercent, @TotalDryWeight, @TotalWetWeight);";
                insertCommand.Parameters.AddWithValue("@Name",currentLoaf.RecipeName);
                insertCommand.Parameters.AddWithValue("@TotalWeight", currentLoaf.TotalWeight);
                insertCommand.Parameters.AddWithValue("@FlourWeight", currentLoaf.FlourWeight);
                insertCommand.Parameters.AddWithValue("@WaterWeight", currentLoaf.WaterWeight);
                insertCommand.Parameters.AddWithValue("@SaltWeight", currentLoaf.SaltWeight);
                insertCommand.Parameters.AddWithValue("@OtherDryWeight", currentLoaf.OtherDryWeight);
                insertCommand.Parameters.AddWithValue("@OtherWetWeight", currentLoaf.OtherWetWeight);
                insertCommand.Parameters.AddWithValue("@Ratio", currentLoaf.Ratio);
                insertCommand.Parameters.AddWithValue("@BakerPercent", currentLoaf.BakerPercent);
                insertCommand.Parameters.AddWithValue("@SaltPercent", currentLoaf.SaltPercent);
                insertCommand.Parameters.AddWithValue("@OtherDryPercent", currentLoaf.OtherDryPercent);
                insertCommand.Parameters.AddWithValue("@TotalDryWeight", currentLoaf.TotalDryWeight);
                insertCommand.Parameters.AddWithValue("@TotalWetWeight", currentLoaf.TotalWetWeight);
                try
                {
                    insertCommand.ExecuteReader();
                }
                catch(SqliteException ex) when (ex.SqliteErrorCode == 19)
                {
                    //Alert overwrite

                    //Assume Yes
                    OverwriteData(currentLoaf);
                }
                db.Close();
            }

        }


        public static List<string> GetData()
        {
            List<String> entries = new List<string>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "BreadRecipes.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                selectCommand.CommandText = "SELECT Name from recipeTable;";

                SqliteDataReader query = selectCommand.ExecuteReader();
                
                while (query.Read())
                {
                    //Here make a Loaf object and copy data from reader.
                    
                    //currentLoaf.TotalWeight = query["totalWeight"];
                    entries.Add(query.GetString(0));
                }

                db.Close();
            }

            return entries;

        }

        public static Loaf GetRecipe(string recipeName)
        {
            Loaf currentLoaf = new Loaf();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "BreadRecipes.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                selectCommand.CommandText = "SELECT * from recipeTable WHERE name = @Name;";
                selectCommand.Parameters.AddWithValue("@Name", recipeName);

                SqliteDataReader query = selectCommand.ExecuteReader();
                if (query != null)
                {
                    while (query.Read())
                    {
                        currentLoaf.RecipeName = query.GetString(1);
                        currentLoaf.TotalWeight = query.GetFloat(2);
                        currentLoaf.FlourWeight = query.GetFloat(3);
                        currentLoaf.WaterWeight = query.GetFloat(4);
                        currentLoaf.SaltWeight = query.GetFloat(5);
                        currentLoaf.OtherDryWeight = query.GetFloat(6);
                        currentLoaf.OtherWetWeight = query.GetFloat(7);
                        currentLoaf.Ratio = query.GetFloat(8);
                        currentLoaf.SaltPercent = query.GetFloat(9);
                        currentLoaf.OtherDryPercent = query.GetFloat(10);
                        currentLoaf.TotalDryWeight = query.GetFloat(11);
                        currentLoaf.TotalWetWeight = query.GetFloat(12);
                        currentLoaf.Notes = query.GetString(13);
                    }
                }
                else throw new SqliteException("query = NULL! Could not populate currentLoaf", 99);

                db.Close();
            }
            return currentLoaf;
        }    

    }
}
