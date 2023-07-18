using SQLSeed.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SQLSeed.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SQLSeed
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ------------------- Load the JSON SQL connection string (Microsoft Users) -------------------
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

            // Access our DataContextDapper class and it's public methods by passing in our connection string to make a connection:
            DataContextDapper dataContextDapper = new DataContextDapper(config);
            
            // Create our "WrestlingTeam.Wrestlers" table:
            string wrestlersTableSqlString = System.IO.File.ReadAllText("Wrestlers.sql");

            dataContextDapper.ExecuteSQL(wrestlersTableSqlString);

            // Load our "Wrestlers.json" JSON data into a string:
            string wrestlersJson = System.IO.File.ReadAllText("Wrestlers.json");

            // Deserialize the file (Convert string to object that inherits from the Wrestlers class):
            IEnumerable<Wrestlers>? wrestlersObject = JsonConvert.DeserializeObject<IEnumerable<Wrestlers>>(wrestlersJson);

            if (wrestlersObject != null)      // If we have entries in the "Wrestlers.json" file, we execute:
            {
                // Open a database connection with the settings specified in "config" variable (which comes from "appsettings.json"):
                using (IDbConnection dbConnection = new SqlConnection(config.GetConnectionString("DefaultConnection")))

                // Generate your SQL statement as a string
                {   
                    string sql_string = "SET IDENTITY_INSERT WrestlingTeam.Wrestlers ON;"       // Make the "WrestlerId" field unprotected
                                        + "INSERT INTO WrestlingTeam.Wrestlers ("
                                        + "WrestlerId,"
                                        + "FirstName,"
                                        + "LastName,"
                                        + "WeightClass"
                                        + ")"
                                        + "VALUES";

                    foreach (Wrestlers singleWrestler in wrestlersObject)
                    {
                        string singleWrestlerString = "("
                                        + "'" + singleWrestler.WrestlerId + "',"
                                        + "'" + singleWrestler.FirstName + "',"
                                        + "'" + singleWrestler.LastName + "',"
                                        + "'" + singleWrestler.WeightClass + "'),";    

                    sql_string += singleWrestlerString;
                    }

                // Remove the very last character from the string (which is an unwanted comma ",") that was added during the last foreach iteration
                sql_string = sql_string.Remove(sql_string.Length -1, 1);
                Console.WriteLine("\n" + sql_string);

                // Execute the statement.  result returns true if table row(s) affected.  If false is returned, the statement didn't work.
                bool result = dataContextDapper.ExecuteSQLBool(sql_string, dbConnection);

                // Reprotect the "WrestlerId" field
                dataContextDapper.ExecuteSQL("SET IDENTITY_INSERT WrestlingTeam.Wrestlers OFF");

                // Figure out what database we are currently in (for potential debugging if we are in the wrong database)
                string currentDatabase = "SELECT DB_NAME() AS [Current Database]";                  // To update a different database, change the connection string in "appsettings.json".
                currentDatabase = dataContextDapper.LoadSQL<string>(currentDatabase);

                // Tell the user if injection of data into the table was successful.
                if (result)
                {
                    Console.WriteLine("The following database has been updated:   " + currentDatabase.ToString());
                    Console.WriteLine("The data has been injected into the TSQL table");    
                }

                }
            } 
        }
    }
}
