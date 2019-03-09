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
        const string sqliteConnString = @"Data Source=LAPTOP-JKSOH6L3;Initial Catalog=advworks.sqlite;Integrated Security=true";
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
            string cName = "advworks.sqlite";

            //var clientProvider = new SqliteSyncProvider(DbHelper.GetDatabaseConnectionString(sqliteConnString));
            //var clientProvider = new SqliteSyncProvider(DbHelper.GetDatabaseConnectionString(cName));
            //var clientProvider = new SqliteSyncProvider(DbHelper.GetDatabaseConnectionString("advworks.sqlite"));
            var clientProvider = new SqliteSyncProvider(cName);

            var proxyClientProvider = new WebProxyClientProvider(new Uri("http://localhost:58515/api/Syncing"));
        
            var agent = new SyncAgent(clientProvider, proxyClientProvider);

            Console.WriteLine("Press a key to start (be sure Web API is running ...)");
            Console.ReadKey();

            do
            {
                Console.Clear();
                Console.WriteLine("Web Sync beginning");
                try
                {                        
                    var s = await agent.SynchronizeAsync();
                    //var s = await agent.SynchronizeAsync();
                    // var c = await agent.SynchronizeAsync(progressS);

                    Console.WriteLine(s);
                    //Console.WriteLine(c);
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