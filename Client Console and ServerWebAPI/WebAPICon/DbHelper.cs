using Dotmim.Sync.Tests.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dotmim.Sync.MySql;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;
using Dotmim.Sync.Web.Server;
using System.Data.Sql;
using Dotmim.Sync;

namespace Dotmim.Sync.SampleConsole
{
    public class DbHelper
    {

        public static string GetDatabaseConnectionString(string dbName) =>
        //$"Data Source=(localdb)\\mssqllocaldb;Initial Catalog={dbName};Integrated Security=true;";
        $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={dbName};Integrated Security=true;";  
        //$"Data Source=(localdb)\\MSSQLLocalDB\\{dbName};Initial Catalog={dbName};Integrated Security=true;";
        //$"Data Source=(localdb)\\MSSQLLocalDB\\{dbName};Integrated Security=true;";
        //$"Data Source=(localdb)\\MSSQLLocalDB\\{dbName};";
        //$"Data Source=(localdb)\\MSSQLLocalDB\\{dbName};Integrated Security=true;";
        // $"Data Source=(localdb)\\mssqllocaldb\\{dbName};Initial Catalog={dbName};Integrated Security=true;";
        // $"Data Source=(localdb)\\mssqllocaldb\\{dbName};Initial Catalog={dbName};";
        //$"Data Source=(localdb)\\mssqllocaldb\\{dbName};Integrated Security=true;";
        // $"Data Source=(localdb)\\mssqllocaldb\\{dbName};";
        //$"Data Source=(localdb)\\mssqllocaldb;";
        //$"Data Source=(localdb)\\mssqllocaldb;Initial Catalog={dbName};";
        //$"Data Source=laptop-jksoh6l3\\{dbName};";
        //$"Data Source=(localdb)\\laptop-jksoh6l3;Initial Catalog={dbName};";
        //$"Data Source=(localdb)\\laptop-jksoh6l3\\{dbName};Integrated Security=true;";
        //$"Data Source=laptop-jksoh6l3\\{dbName};Initial Catalog={dbName};Integrated Security=true;";
        //$"Data Source=(localdb)\\laptop-jksoh6l3;Initial Catalog={dbName};Integrated Security=true;";

        /// <summary>
        /// create a server database with datas and an empty client database
        /// </summary>
        /// <returns></returns>
        public static async Task EnsureDatabasesAsync(string databaseName, bool useSeeding = true)
        {
            // Create server database with items
            using (var dbServer = new AdventureWorksContext(GetDatabaseConnectionString(databaseName), useSeeding))
            {
                await dbServer.Database.EnsureDeletedAsync();
                await dbServer.Database.EnsureCreatedAsync();
            }
        }

        public static async Task DeleteDatabaseAsync(string dbName)
        {
            var masterConnection = new SqlConnection(GetDatabaseConnectionString("master"));
            await masterConnection.OpenAsync();
            var cmdDb = new SqlCommand(GetDeleteDatabaseScript(dbName), masterConnection);
            await cmdDb.ExecuteNonQueryAsync();
            masterConnection.Close();
        }

      

        public static async Task CreateDatabaseAsync(string dbName, bool recreateDb = true)
        {
            var masterConnection = new SqlConnection(GetDatabaseConnectionString("master"));
            await masterConnection.OpenAsync();
            var cmdDb = new SqlCommand(GetCreationDBScript(dbName, recreateDb), masterConnection);
            await cmdDb.ExecuteNonQueryAsync();
            masterConnection.Close();
        }

        private static string GetDeleteDatabaseScript(string dbName) =>
                  $@"if (exists (Select * from sys.databases where name = '{dbName}'))
            begin
	            alter database [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	            drop database {dbName}
            end";


        //Master
        private static string GetCreationDBScript(string dbName, bool recreateDb = true)
        {
            if (recreateDb)
                return $@"if (exists (Select * from sys.databases where name = '{dbName}'))
                    begin
	                    alter database [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	                    drop database {dbName}
                    end
                    Create database {dbName}";
            else
                return $@"if not (exists (Select * from sys.databases where name = '{dbName}')) 
                          Create database {dbName}";

        }

    }
}
