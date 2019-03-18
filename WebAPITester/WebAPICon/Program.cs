using System;
using Dotmim.Sync;
using Dotmim.Sync.Enumerations;
using Dotmim.Sync.SampleConsole;
using Dotmim.Sync.Web.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Dotmim.Sync.Web.Server;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SerializationFormat = Dotmim.Sync.Enumerations.SerializationFormat;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.SqlServer;

namespace WebAPIConsoleTest
{
   internal class Program
    {
        const string sqlConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks;Integrated Security=true";

        //const string sqliteConnString = @"Data Source=LAPTOP-JKSOH6L3\advworks.sqlite;Integrated Security=true";
        //const string sqliteConnString = @"Data Source=LAPTOP-JKSOH6L3;Initial Catalog=advworks.sqlite;Integrated Security=true";
        const string sqliteConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=advworks.sqlite;Integrated Security=true";
        //const string sqliteConnString = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=true";
        //const string sqliteConnString = @"Data Source=(localdb)\MSSQLLocalDB\advworks.sqlite;Initial Catalog=advworks.sqlite;Integrated Security=true";
        //const string sqliteConnString = @"Data Source=LAPTOP-JKSOH6L3;Integrated Security=true";

        public static string[] allTables = new string[] {"ProductCategory","Address",
                                                     "Product","ProductModel",
                                                     "Customer", "CustomerAddress",
                                                      "SalesOrderDetail","SalesOrderHeader"
                                                    };

        private static void Main(string[] args)
        {
            TestWebAPISync().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        private static async Task TestWebAPISync()
        {
            //string cName = sqliteConnString;
            string cName = "advworks.sqlite";

            var sqliteSyncClient = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(sqliteConnString));//This method works perfectly.
            var clientProvider = new SqliteSyncProvider(cName);//This method works - Almost

            //var clientProvider = new SqliteSyncProvider(DbHelper.GetDatabaseConnectionString(sqliteConnString)); //Doesn't work
            //var clientProvider = new SqliteSyncProvider(DbHelper.GetDatabaseConnectionString(cName)); //Doesn't work
            //var clientProvider = new SqliteSyncProvider(sqliteConnString);  //Doesn't work

            var proxyClientProvider = new WebProxyClientProvider(new Uri("http://localhost:58515/api/Syncing"));
        
            var agent = new SyncAgent(sqliteSyncClient, proxyClientProvider); //SQLite Client to SQL Server

            Console.WriteLine("Press a key to start (be sure Web API is running ...)");
            Console.ReadKey();

            do
            {
                Console.Clear();
                Console.WriteLine("Web Sync beginning");
                try
                {

                    var progressS = new Progress<ProgressArgs>(pa => Console.WriteLine($"{pa.Context.SessionId} - {pa.Context.SyncStage}\t {pa.Message}"));

                   // var s = await agent.SynchronizeAsync();
                    //var s = await agent.SynchronizeAsync();
                    var c = await agent.SynchronizeAsync(progressS);

                    //Console.WriteLine(s);
                    Console.WriteLine(c);
                }
                catch (SyncException e)
                {
                    Console.WriteLine(e.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("UNKNOWN EXCEPTION : " + e.Message);
                }

                Console.WriteLine("Sync Ended. Press a key to start again, or Escape to end");

            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            Console.WriteLine("End");
        }
    }
  }