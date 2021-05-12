using System;
using System.Collections.Generic;
using System.IO;
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

                String tableCommandOne = "CREATE TABLE IF NOT EXISTS \"recipeTable\"" + DBTEMPLATE;    

                SqliteCommand createTableOne = new SqliteCommand(tableCommandOne, db);
                createTableOne.ExecuteReader();

                String tableCommandTwo = "CREATE TABLE IF NOT EXISTS \"persistenceTable\"" + DBTEMPLATE;
                SqliteCommand createTableTwo = new SqliteCommand(tableCommandTwo, db);
                createTableTwo.ExecuteReader();
            }
           
        }

        public static Loaf GetPreviousState()
        {
            // Called when loaded after Initializing database to load previous state
            // To keep persistence
                 
            List<string> lastEntry = GetRecipeListFromDatabase("persistenceTable");


            if (lastEntry.Count < 1)
            {
                //Nothing to load
                Loaf newLoaf = new Loaf();
                return newLoaf;
            }

            Loaf previousLoaf = GetRecipeFromDatabaseByName(lastEntry[0], "persistenceTable");
            return previousLoaf;

        }
            
    
        //SaveCurrentState will save either the loaf passed to it, or the last saved loaf
        public static void SaveCurrentState(Loaf saveLoaf = null)
        {
            if (saveLoaf != null)
            {
                // Saves the current state as the only entry in persistenceTable
                ClearAllDataFromPersistenceTable();
                AddEntryToDatabase(saveLoaf, "persistenceTable");
            }
            else 
            {
                ClearAllDataFromPersistenceTable();
                Loaf currentLoaf = new Loaf();
                AddEntryToDatabase(currentLoaf, "persistenceTable");
            }
        }
        
        public static void DeleteEntryFromDatabaseByName(Loaf currentLoaf, string table)
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
                    deleteCommand.CommandText = "DELETE FROM " +table+ " WHERE Name = @Name;";
                    deleteCommand.Parameters.AddWithValue("@Name", currentLoaf.RecipeName);
                    deleteCommand.ExecuteReader();
                    db.Close();
                }
            }
        }

        public static void OverwriteData(Loaf currentLoaf, string table)
        {
                DeleteEntryFromDatabaseByName(currentLoaf, table);
                AddEntryToDatabase(currentLoaf, table);
        }
        public static void AddEntryToDatabase(Loaf currentLoaf, string table)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "BreadRecipes.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                // Use parameterized query to prevent SQL injection attacks
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                insertCommand.CommandText = "INSERT INTO " + table + " VALUES (NULL,@Name,"+
                    "@TotalWeight, @FlourWeight, @WaterWeight, @SaltWeight, " +
                    "@OtherDryWeight, @OtherWetWeight, @Ratio, @SaltPercent, " + 
                    "@OtherDryPercent, @TotalDryWeight, @TotalWetWeight, @Notes);";
                insertCommand.Parameters.AddWithValue("@Name",currentLoaf.RecipeName);
                insertCommand.Parameters.AddWithValue("@TotalWeight", currentLoaf.TotalWeight);
                insertCommand.Parameters.AddWithValue("@FlourWeight", currentLoaf.FlourWeight);
                insertCommand.Parameters.AddWithValue("@WaterWeight", currentLoaf.WaterWeight);
                insertCommand.Parameters.AddWithValue("@SaltWeight", currentLoaf.SaltWeight);
                insertCommand.Parameters.AddWithValue("@OtherDryWeight", currentLoaf.OtherDryWeight);
                insertCommand.Parameters.AddWithValue("@OtherWetWeight", currentLoaf.OtherWetWeight);
                insertCommand.Parameters.AddWithValue("@Ratio", currentLoaf.Ratio);
                insertCommand.Parameters.AddWithValue("@SaltPercent", currentLoaf.SaltPercent);
                insertCommand.Parameters.AddWithValue("@OtherDryPercent", currentLoaf.OtherDryPercent);
                insertCommand.Parameters.AddWithValue("@TotalDryWeight", currentLoaf.TotalDryWeight);
                insertCommand.Parameters.AddWithValue("@TotalWetWeight", currentLoaf.TotalWetWeight);
                insertCommand.Parameters.AddWithValue("@Notes", currentLoaf.Notes);
                try
                {
                    insertCommand.ExecuteReader();
                }

                //Sqlite ErrorCode 19 - (Name) value is not unique but should be.
                catch(SqliteException ex) when (ex.SqliteErrorCode == 19)
                {
                    // Alert overwrite is done in view controller.

                    // So go ahead and overwrite
                    OverwriteData(currentLoaf, table);
                }
                db.Close();
            }

        }

        private static void ClearAllDataFromPersistenceTable()
        {
            // Deletes all databaseNames from a specified table in the database
            // BE CAREFUL! 'persistenceTable' is hard coded for security reasons.
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "BreadRecipes.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                deleteCommand.CommandText = "DELETE FROM persistenceTable;";
                deleteCommand.ExecuteReader();
                db.Close();
            }

        }


        public static List<string> GetRecipeListFromDatabase(string table = "recipeTable")
        {
            List<String> databaseNames = new List<string>();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "BreadRecipes.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                // SQL command get the Name value of all databaseNames in the database
                selectCommand.CommandText = "SELECT Name from " + table +";";

                SqliteDataReader query = selectCommand.ExecuteReader();
                
                while (query.Read())
                {
                    //Add each Name to the databaseNames list
                    databaseNames.Add(query.GetString(0));
                }

                db.Close();
            }
            return databaseNames;

        }

        public static Loaf GetRecipeFromDatabaseByName(string recipeName, string table = "recipeTable")
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
                selectCommand.CommandText = "SELECT * from " + table +" WHERE name = @Name;";
                selectCommand.Parameters.AddWithValue("@Name", recipeName);

                SqliteDataReader query = selectCommand.ExecuteReader();
                if (query != null)
                {
                    while (query.Read())
                    {
                        currentLoaf.Key = query.GetInt32(0);
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
                else throw new SqliteException("query = NULL! Could not populate currentLoaf", 1);

                db.Close();
            }
            return currentLoaf;
        }    

    }
}
