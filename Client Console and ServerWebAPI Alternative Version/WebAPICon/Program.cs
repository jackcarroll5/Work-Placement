using System;
using System.Threading.Tasks;
using Dotmim.Sync;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.SqlServer;
using Dotmim.Sync.Web.Client;

namespace WebAPIConsoleTest
{
    internal class Program
    {
      

        //const string sqliteConnString = @"Data Source=LAPTOP-JKSOH6L3\advworks.sqlite;Integrated Security=true";
        //const string sqliteConnString = @"Data Source=LAPTOP-JKSOH6L3;Initial Catalog=advworks.sqlite;Integrated Security=true";

        //const string sqliteConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=advworks.sqlite;Integrated Security=true";
        //const string sqliteConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=adventureworks.sqlite;Integrated Security=true";
        //const string sqliteConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=adventureworks.sqlite";
        //const string sqliteConnString = @"Data Source=(localdb)\MSSQLLocalDB\adventureworks.sqlite;Initial Catalog=adventureworks.sqlite;Integrated Security=true";
        //const string sqliteConnString = @"Data Source=(localdb)\MSSQLLocalDB\adventureworks.sqlite;Integrated Security=true";
        //const string sqliteConnString = @"Data Source=(localdb)\MSSQLLocalDB\adventureworks.sqlite";
        //const string sqliteConnString = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=true";
        //const string sqliteConnString = @"Data Source=(localdb)\MSSQLLocalDB\advworks.sqlite;Initial Catalog=advworks.sqlite;Integrated Security=true";
        //const string sqliteConnString = @"Data Source=LAPTOP-JKSOH6L3;Integrated Security=true";


        //const string sqliteConnString = @"Data Source=C:\Users\Jack\Desktop\WebAPITester\adventureworks.sqlite";
        const string sqliteConnString = @"Data Source=C:\Users\Jack\Desktop\Client Console and ServerWebAPI\advenworks.sqlite";

        const string sqlConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks;Integrated Security=true;MultipleActiveResultSets=true;";

        public static string[] allTables = new string[] {"ProductCategory",
                                                     "Product","ProductModel", "Address",
                                                     "Customer", "CustomerAddress",
                                                    "SalesOrderHeader", "SalesOrderDetail"};


        private static void Main(string[] args)
        {
            TestWebAPISync().GetAwaiter().GetResult();
            Console.ReadLine();
        }

        private static async Task TestWebAPISync()
        {
            //string cName = sqliteConnString;
            //string cName = "advworks.sqlite";
            //string cName = "C:\\Users\\Jack\\Desktop\\Client Console and ServerWebAPI\\adventureworks.sqlite";
            string cName = "C:\\Users\\Jack\\Desktop\\Client Console and ServerWebAPI\\adventworks.sqlite";
            //string cName = "C:\\Users\\Jack\\Desktop\\Client Console and ServerWebAPI\\advenworks.sqlite";


            var sqlSync = new SqlSyncProvider(sqlConnString);

            //var sqliteSyncClient = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(sqliteConnString));//This method works perfectly.
            var sqliteSyncClient = new SqliteSyncProvider(cName);//This method works - Almost
            //var clientProvider = new SqliteSyncProvider(DbHelper.GetDatabaseConnectionString(sqliteConnString)); //Doesn't work
            //var clientProvider = new SqliteSyncProvider(DbHelper.GetDatabaseConnectionString(cName)); //Doesn't work
            //var clientProvider = new SqliteSyncProvider(sqliteConnString);  //Doesn't work

            var proxyClientProvider = new WebProxyClientProvider(new Uri("http://localhost:58515/api/Sync"));

            //var agent = new SyncAgent(sqliteSyncClient, proxyClientProvider); //SQLite Client to SQL Server

            var agent = new SyncAgent(sqliteSyncClient, proxyClientProvider); //Method for Web API SQLite Client to SQL Server
            //var agent = new SyncAgent(sqliteSyncClient, sqlSync, allTables); //Alt for Console - SQLite Client to SQL Server

            //agent.ApplyChangedFailed = (int)Agent_AppliedFailed;

            agent.SetConfiguration(s =>
            {
                s.ScopeInfoTableName = "tscopeinfo";
                s.SerializationFormat = Dotmim.Sync.Enumerations.SerializationFormat.Binary;
                s.StoredProceduresPrefix = "s";
                s.StoredProceduresSuffix = "";
                s.TrackingTablesPrefix = "t";
                s.TrackingTablesSuffix = "";
            });

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