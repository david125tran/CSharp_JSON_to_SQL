using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SQLSeed.Data
{
    public class DataContextDapper
    {
        private readonly IConfiguration _config;
        public DataContextDapper(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// This class takes one SQL statement as a string and executes it.  It returns a 1 if a row was affected.
        /// </summary>
        public int ExecuteSQL(string sql_string)
        {
            using (IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                return dbConnection.Execute(sql_string);
            }
        }

        /// <summary>
        /// This class takes one SQL statement as a string and executes it.  It returns true if a row was affected.
        /// And returns false if a row was not affected.
        /// </summary>
        public bool ExecuteSQLBool(string sql_string, IDbConnection dbConnection)
        {
            return (dbConnection.Execute(sql_string) > 0);
        }

        /// <summary>
        /// This class takes one SQL statement as any data type.  You must explicitly state which data type you 
        /// want.
        /// </summary>
        public T LoadSQL <T> (string sql_statement)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            // ".Query" will return a full list of the object
            return dbConnection.QuerySingle<T>(sql_statement);
        }


    }
}