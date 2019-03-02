using Dotmim.Sync;
using Dotmim.Sync.Enumerations;
using Dotmim.Sync.SampleConsole;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.SqlServer;
using Dotmim.Sync.Tests.Models;
using Dotmim.Sync.Web.Client;
using Dotmim.Sync.Web.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SerializationFormat = Dotmim.Sync.Enumerations.SerializationFormat;

internal class Program
{
    public static string serverDbName = "Server";
    public static string clientDbName = "Client";

    //const string sqlConnString = @"Data Source=LAPTOP-JKSOH6L3\SQLEXPRESS;Initial Catalog=AdventureWorks;Integrated Security=true";
    const string sqlConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks;Integrated Security=true";


    const string sqliteConnString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=advworks.sqlite;Integrated Security=true";


    /*Configure schemas directly*/
    public static string[] allTables = new string[] {"ProductCategory","Address",
                                                     "Product","ProductModel",
                                                     "Customer", "CustomerAddress",
                                                      "SalesOrderDetail","SalesOrderHeader"
                                                    };

    private static void Main(string[] args)
    {
       //SynchronizeExistingTablesAsync().GetAwaiter().GetResult();
       //TestSyncThroughWebApi().GetAwaiter().GetResult();
        //SynchronizeOSAsync().GetAwaiter().GetResult();
         SyncHttpThroughKestellAsync().GetAwaiter().GetResult();
        //SynchronizeAsync().GetAwaiter().GetResult();
        //RunAsync().GetAwaiter().GetResult();
        Console.ReadLine();
    }

    private async static Task RunAsync()
    {
        // Create databases 
        await DbHelper.EnsureDatabasesAsync(serverDbName);
        await DbHelper.CreateDatabaseAsync(clientDbName);

        // Launch Sync
        await SynchronizeAsync();
    }

    /// <summary>
    /// * Launch a simple sync, over TCP network, each sql server (client and server are reachable through TCP cp
    /// </summary>
    /// <returns></returns>
    private static async Task SynchronizeAsync()
    {
        // Create 2 Sql Sync providers
        var serverProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(sqlConnString));//SQL Server
        //var serverProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(serverDbName));//SQL Server
       // var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(clientDbName));//SQLite
        var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(sqliteConnString));//SQLite

        // Tables involved in the sync process:
        var tables = allTables;

        /*await clientProvider.ProvisionAsync(tables,
            SyncProvision.StoredProcedures
           | SyncProvision.TrackingTable
           | SyncProvision.Triggers);


        await clientProvider.DeprovisionAsync(tables,
            SyncProvision.StoredProcedures
           | SyncProvision.TrackingTable
           | SyncProvision.Triggers);*/

        // Creating an agent that will handle all the process
        var agent = new SyncAgent(clientProvider, serverProvider, tables);

        // Using the Progress pattern to handle progession during the synchronization
        var progress = new Progress<ProgressArgs>(s => Console.WriteLine($"[client]: {s.Context.SyncStage}:\t{s.Message}"));


        // Setting configuration options
        agent.SetConfiguration(s =>
        {
            s.ScopeInfoTableName = "tscopeinfo";
            s.SerializationFormat = Dotmim.Sync.Enumerations.SerializationFormat.Binary;
            s.StoredProceduresPrefix = "s";
            s.StoredProceduresSuffix = "";
            s.TrackingTablesPrefix = "t";
            s.TrackingTablesSuffix = "";
        });

        agent.SetOptions(opt =>
        {
            opt.BatchDirectory = Path.Combine(SyncOptions.GetDefaultUserBatchDiretory(), "sync");
            opt.BatchSize = 100;
            opt.CleanMetadatas = true;
            opt.UseBulkOperations = true;
            opt.UseVerboseErrors = false;
        });


        do
        {
            Console.Clear();
            Console.WriteLine("Sync Starts");
            try
            {
                //Check Server progress
                //agent.RemoteProvider.SetProgress(progress);
                // Launch the sync process
                var s1 = await agent.SynchronizeAsync(progress);//SyncType.Normal
               // SyncType.Reinitialize
               //SyncType.ReinitializeWithUpload

                // Write results
                Console.WriteLine(s1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            //Console.WriteLine("Sync Ended. Press a key to start again, or Escapte to end");
        } while (Console.ReadKey().Key != ConsoleKey.Escape);

        Console.WriteLine("End");
    }

    /// <summary>
    /// Launch a simple sync, over TCP network, each sql server (client and server are reachable through TCP cp
    /// </summary>
    /// <returns></returns>
    private static async Task SynchronizeExistingTablesAsync()
    {
        //Tackling progress feedback and get progress from server and client
        //string serverName = "ServerTablesExist";
        string serverName = sqlConnString;
        //string clientName = "ClientsTablesExist";
        string cName = "advworks.sqlite";

        await DbHelper.EnsureDatabasesAsync(serverName);
        //await DbHelper.EnsureDatabasesAsync(clientName);
        await DbHelper.EnsureDatabasesAsync(cName);

        // Create 2 Sql Sync providers
        var serverProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(serverName));
       // var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(clientName));
        var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(cName));

        // Tables involved in the sync process:
        var tables = allTables;

        // Creating an agent that will handle all the process
        /*var agent = new SyncAgent(clientProvider, serverProvider, new string[] {
        "Customer","CustomerAddress"});*/   
        /*var agent = new SyncAgent(clientProvider, serverProvider, new string[] {
        "ProductCategory","ProductModel","Product","Address",
        "Customer","CustomerAddress"});*/
        /*var agent = new SyncAgent(clientProvider, serverProvider, new string[] {
        "ProductCategory","Product"});*/
        /*var agent = new SyncAgent(clientProvider, serverProvider, new string[] {
        "Customer"});*/

        /*var agent = new SyncAgent(clientProvider, serverProvider, new string[] {
        "CustomerAddress"});*/
        var agent = new SyncAgent(clientProvider, serverProvider, tables);

        //var agent = new SyncAgent(clientProvider, serverProvider, configuration);

        //agent.ApplyChangedFailed += Agent_ApplyChangedFailed;

        //From SyncConfiguration action
        /*agent.SetConfiguration(c =>
        {
          
            c["Address"].Schema = "SalesLT";
            c["Customer"].Schema = "SalesLT";
            c["CustomerAddress"].Schema = "SalesLT";
        });*/

        /*agent.SetConfiguration(c =>
        {
            c["Address"].Schema = SyncDirection.DownloadOnly;
            c["Customer"].Schema = SyncDirection.DownloadOnly;
            c["CustomerAddress"].Schema = SyncDirection.DownloadOnly;
        });*/

       /*await clientProvider.ProvisionAsync(tables,
            SyncProvision.StoredProcedures
           | SyncProvision.TrackingTable
           | SyncProvision.Triggers);*/


         /*await clientProvider.DeprovisionAsync(tables,
            SyncProvision.StoredProcedures
           | SyncProvision.TrackingTable
           | SyncProvision.Triggers);*/

        // Using the Progress pattern to handle progession during the synchronization
        var progress = new Progress<ProgressArgs>(s => Console.WriteLine($"[client]: {s.Context.SyncStage}:\t{s.Message}"));
        var progressS = new Progress<ProgressArgs>(s => Console.WriteLine($"[server]: {s.Context.SyncStage}:\t{s.Message}"));


        /*Server winner of any conflict - Server Wins*/
        agent.SetConfiguration(c =>
        {
            c.ConflictResolutionPolicy = ConflictResolutionPolicy.ServerWins;
        });


        // Setting configuration options
        agent.SetConfiguration(s =>
        {
            s.ScopeInfoTableName = "tscopeinfo";
            s.SerializationFormat = SerializationFormat.Binary;
            s.StoredProceduresPrefix = "s";
            s.StoredProceduresSuffix = "";
            s.TrackingTablesPrefix = "t";
            s.TrackingTablesSuffix = "";
        });

      
        //Values on client and server = Different
        //Client same as TCP mode
        agent.SetOptions(opt =>
        {
            opt.BatchDirectory = Path.Combine(SyncOptions.GetDefaultUserBatchDiretory(), "sync");
            opt.BatchSize = 100;
            opt.CleanMetadatas = true;
            opt.UseBulkOperations = true;
            opt.UseVerboseErrors = false;
        });


        /*agent.SetOptions(opt =>
        {
            opt.BatchDirectory = Path.Combine(SyncOptions.GetDefaultUserBatchDiretory(), "client");
            opt.BatchSize = 100;
        });*/

        //Manually resolve conflict based on column value
        agent.LocalProvider.InterceptApplyChangesFailed(e =>
        { 
        if (e.Conflict.RemoteRow.Table.TableName == "Product")
        {
                e.Action = (int)e.Conflict.RemoteRow["ProductModelID"] == 6 ? ConflictResolution.ClientWins : ConflictResolution.ServerWins;
        }
         });

        agent.LocalProvider.InterceptApplyChangesFailed(e =>
        {
            switch (e.Conflict.Type)
            {
                case ConflictType.RemoteUpdateLocalInsert:
                case ConflictType.RemoteUpdateLocalUpdate:
                case ConflictType.RemoteUpdateLocalDelete:
                case ConflictType.RemoteInsertLocalInsert:
                case ConflictType.RemoteInsertLocalUpdate:
                case ConflictType.RemoteInsertLocalDelete:
                    e.Action = ConflictResolution.ServerWins;
                    break;
                case ConflictType.RemoteUpdateLocalNoRow:
                case ConflictType.RemoteInsertLocalNoRow:
                case ConflictType.RemoteDeleteLocalNoRow:
                    e.Action = ConflictResolution.ClientWins;
                    break;
            }
        });

        //On Server Side to generate required stored procedures - Add Filter
        agent.SetConfiguration(c => c.Filters.Add("Customers","CustomerID"));


        SyncConfiguration config = new SyncConfiguration(new[] { "Customer" });
        config.Filters.Add("Customer", "NameStyle");

        //Customers when namestyle = 0 are synchronized
        agent.Parameters.Add("Product", "Color","Black");


        agent.LocalProvider.InterceptApplyChangesFailed(e =>
        {
            if (e.Conflict.RemoteRow.Table.TableName == "Product")
            {
                e.Action = ConflictResolution.MergeRow;
                e.FinalRow["ProductCategoryID"] = "MOUNTB";
            }
        });

       /*agent = new SyncAgent(clientProvider, serverProvider, (conf =>{
    conf.Add(new string[] { "ServiceTickets" })
}));*/


         var remoteProvider = agent.RemoteProvider as CoreProvider;

        var dpAction = new Action<DatabaseProvisionedArgs>(args =>
        {
            Console.WriteLine($"-- [InterceptDatabaseProvisioned] -- ");

            var sql = $"Update tscopeinfo set scope_last_sync_timestamp = 0 where [scope_is_local] = 1";

            var cmd = args.Connection.CreateCommand();
            cmd.Transaction = args.Transaction;
            cmd.CommandText = sql;

            cmd.ExecuteNonQuery();

        });

        remoteProvider.InterceptDatabaseProvisioned(dpAction);

        agent.LocalProvider.InterceptDatabaseProvisioned(dpAction);

       
        agent.LocalProvider.InterceptTableChangesSelected((args) =>
        {
            Console.WriteLine($"Changes selected from table {args.TableChangesSelected.TableName}: ");
            Console.WriteLine($"\tInserts:{args.TableChangesSelected.Inserts}: ");
            Console.WriteLine($"\tUpdates: {args.TableChangesSelected.Updates}: ");
            Console.WriteLine($"\tDeletes: {args.TableChangesSelected.Deletes}: ");

        });


        /*Detailed Logs*/
        agent.LocalProvider.InterceptTableChangesSelecting((args) =>
        {
            Console.WriteLine($"Get changes for table {args.TableName}");
        });


        /*Intercept Stages - Not synchronize a table*/
       /* agent.LocalProvider.InterceptTableChangesApplying((args) =>
        {
            if (args.TableName == "Product")
                args.Action = ChangeApplicationAction.Rollback;
        });*/

        do
        {
            Console.Clear();
            Console.WriteLine("Sync Starting");
            try
            {
                agent.RemoteProvider.SetProgress(progress);
                agent.RemoteProvider.SetProgress(progressS);
                // Launch the sync process
                var s1 = await agent.SynchronizeAsync(progress);
                //var s1 = await agent.SynchronizeAsync(SyncType.Normal);
                //var s1 = await agent.SynchronizeAsync(SyncType.Reinitialize);
                //var s1 = await agent.SynchronizeAsync(SyncType.ReinitializeWithUpload);
                Console.WriteLine("\n");
                var s2 = await agent.SynchronizeAsync(progressS);
                

                // Write results
                Console.WriteLine(s1);//Client 
                Console.WriteLine("\n");
                Console.WriteLine(s2);//Server
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Sync Ended. Press a key to start again, or Esc to end");
        } while (Console.ReadKey().Key != ConsoleKey.Escape);

        Console.WriteLine("End");
    }



    private static async Task SynchronizeOSAsync()
    {
        // Create 2 Sql Sync providers
        //var serverProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString("OptionsServer"));

        //var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString("OptionsClient"));
        //var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(sqlConnString));
        //var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(clientDbName));


        var serverProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(sqlConnString));
        var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(sqliteConnString));

        // Tables involved in the sync process:
        //var tables = new string[] { "ObjectSettings", "ObjectSettingValues" };
        var tables = new string[] {"Product","ProductModel" };

        // Creating an agent that will handle all the process
        var agent = new SyncAgent(clientProvider, serverProvider, tables);

        // Using the Progress pattern to handle progession during the synchronization
        var progress = new Progress<ProgressArgs>(s => Console.WriteLine($"[client]: {s.Context.SyncStage}:\t{s.Message}"));

        do
        {
            Console.Clear();
            Console.WriteLine("Sync Starter");
            try
            {
                // Launch the sync process
                var s1 = await agent.SynchronizeAsync(progress);

                // Write results
                Console.WriteLine(s1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            //Console.WriteLine("Sync Ended. Press a key to start again, or Escapte to end");
        } while (Console.ReadKey().Key != ConsoleKey.Escape);

        Console.WriteLine("End");
    }




    public static async Task SyncHttpThroughKestellAsync()
    {
        string cName = "advworks.sqlite";

        // server provider
        //var serverProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(serverDbName));
        var serverProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(sqlConnString));

        // client provider
       // var clientProvider = new SqliteSyncProvider(DbHelper.GetDatabaseConnectionString(sqliteConnString));
        var clientProvider = new SqliteSyncProvider(DbHelper.GetDatabaseConnectionString(cName));
        // var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(sqliteConnString));

        // proxy client provider 
        var proxyClientProvider = new WebProxyClientProvider();

        //await DbHelper.EnsureDatabasesAsync(sqlConnString);
        //await DbHelper.EnsureDatabasesAsync(sqliteConnString);
     
        var tables = new string[] {"ProductCategory",
                "ProductModel",
                "Product",
                "Address", "Customer", "CustomerAddress",
                "SalesOrderHeader", "SalesOrderDetail" };

        var configuration = new Action<SyncConfiguration>(conf =>
        {
            conf.ScopeName = "AdventureWorks";
            conf.ScopeInfoTableName = "tscopeinfo";
            conf.SerializationFormat = SerializationFormat.Binary;
            conf.StoredProceduresPrefix = "s";
            conf.StoredProceduresSuffix = "";
            conf.TrackingTablesPrefix = "t";
            conf.TrackingTablesSuffix = "";
            conf.Add(tables);
        });


        var optionsClient = new Action<SyncOptions>(opt =>
        {
            opt.BatchDirectory = Path.Combine(SyncOptions.GetDefaultUserBatchDiretory(), "client");
            opt.BatchSize = 100;
            opt.CleanMetadatas = true;
            opt.UseBulkOperations = true;
            opt.UseVerboseErrors = false;

        });

        var optionsServer = new Action<SyncOptions>(opt =>
        {
            opt.BatchDirectory = Path.Combine(SyncOptions.GetDefaultUserBatchDiretory(), "server");
            opt.BatchSize = 100;
            opt.CleanMetadatas = true;
            opt.UseBulkOperations = true;
            opt.UseVerboseErrors = false;

        });

        var serverHandler = new RequestDelegate(async context =>
        {
            var proxyServerProvider = WebProxyServerProvider.Create(context, serverProvider, configuration, optionsServer);

            await proxyServerProvider.HandleRequestAsync(context);
        });

        using (var server = new KestrellTestServer())
        {
            var clientHandler = new ResponseDelegate(async (serviceUri) =>
            {
               proxyClientProvider.ServiceUri = new Uri(serviceUri);

                var syncAgent = new SyncAgent(clientProvider, proxyClientProvider);

                //var progress = new Progress<ProgressArgs>(s => Console.WriteLine($"[client]: {s.Context.SyncStage}:\t{s.Message}"));

               // syncAgent.SetProgress(progress);

                do
                {
                    Console.Clear();
                    Console.WriteLine("Sync Starts Right Now");
                    try
                    {
                        var cts = new CancellationTokenSource();

                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("1 : Normal synchronization.");
                        Console.WriteLine("2 : Synchronization with reinitialize");
                        Console.WriteLine("3 : Synchronization with upload and reinitialize");
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("What's your choice ? ");
                        Console.WriteLine("--------------------------------------------------");
                        var choice = Console.ReadLine();

                        if (int.TryParse(choice, out var choiceNumber))
                        {
                            Console.WriteLine($"You choose {choice}. Start operation....");
                            switch (choiceNumber)
                            {
                                case 1:
                                    var s1 = await syncAgent.SynchronizeAsync(cts.Token);
                                    Console.WriteLine(s1);
                                    break;
                                case 2:
                                    s1 = await syncAgent.SynchronizeAsync(SyncType.Reinitialize, cts.Token);
                                    Console.WriteLine(s1);
                                    break;
                                case 3:
                                    s1 = await syncAgent.SynchronizeAsync(SyncType.ReinitializeWithUpload, cts.Token);
                                    Console.WriteLine(s1);
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                    catch (SyncException e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("UNKNOWN EXCEPTION : " + e.Message);
                    }

                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("Press a key to choose again, or Escapte to end");

                } while (Console.ReadKey().Key != ConsoleKey.Escape);

            });
            await server.Run(serverHandler, clientHandler);
        }

    }

    /// <summary>
    /// * Test a client syncing through a Web API
    /// </summary>
    private static async Task TestSyncThroughWebApi()
    {
      
        string sName = sqlConnString;
        //string cName = "advworks.sqlite";
    
        //await DbHelper.EnsureDatabasesAsync(sName);
       // await DbHelper.EnsureDatabasesAsync(cName);

  
        var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(sName));
        //var clientProvider = new SqlSyncProvider(DbHelper.GetDatabaseConnectionString(sqlConnString));

        /*Client*/
        var sqliteSync = new SqliteSyncProvider("advworks.sqlite");
        //var sqliteSync = new SqliteSyncProvider(DbHelper.GetDatabaseConnectionString(sqliteConnString));

       /*Server*/ var proxyClientProvider = new WebProxyClientProvider(
            new Uri("http://localhost:52288/api/Sync"));

        var agent = new SyncAgent(clientProvider, proxyClientProvider);//SQL Client to SQLite Server
      //var agent = new SyncAgent(sqliteSync, proxyClientProvider);//SQLite Client to SQLite Server
      
        //agent.ApplyChangedFailed += Agent_ApplyChangedFailed;
 
      
        var progress = new Progress<ProgressArgs>(pa => Console.WriteLine($"{pa.Context.SessionId} - {pa.Context.SyncStage}\t {pa.Message}"));
        var progressS = new Progress<ProgressArgs>(pa => Console.WriteLine($"{pa.Context.SessionId} - {pa.Context.SyncStage}\t {pa.Message}"));

        Console.WriteLine("Press a key to start (be sure Web API is running ...)");
        Console.ReadKey();

        do
        {
            Console.Clear();
            Console.WriteLine("Web Sync starting");
            try
            {   
                /*Client Side*/           
                agent.RemoteProvider.SetProgress(progress);
                
                //agent.RemoteProvider.SetProgress(progressS);

                var s = await agent.SynchronizeAsync(progress);
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

   /*private static async Task AlterSchemasASync()
      {
          var serverProvider = new SqlSyncProvider(sqlConnString);
          var clientProvider = new SqlSyncProvider(sqliteConnString);

          // tables to edit
          var tables = new string[] { "Customers" };

          // delete triggers and sp
          await serverProvider.DeprovisionAsync(tables, SyncProvision.StoredProcedures | SyncProvision.Triggers);
          await clientProvider.DeprovisionAsync(tables, SyncProvision.StoredProcedures | SyncProvision.Triggers);

          // use whatever you want to edit your schema
          // add column on server
          using (var cs = serverProvider.CreateConnection() as SqlConnection)
          {
              cs.Open();
              var cmd = new SqlCommand("ALTER TABLE dbo.Customer ADD Comments nvarchar(50) NULL", cs);
              cmd.ExecuteNonQuery();
              cs.Close();
          }
          // add column on client
          using (var cs = clientProvider.CreateConnection() as SqlConnection)
          {
              cs.Open();
              var cmd = new SqlCommand("ALTER TABLE dbo.Customer ADD Comments nvarchar(50) NULL", cs);
              cmd.ExecuteNonQuery();
              cs.Close();
          }

          // Provision again
          await serverProvider.ProvisionAsync(tables, SyncProvision.StoredProcedures | SyncProvision.Triggers);
          await clientProvider.ProvisionAsync(tables, SyncProvision.StoredProcedures | SyncProvision.Triggers);

          // sync !
          await SynchronizeAsync();
      }*/

    public void AddingDatas(DbConnection connection)
    {

        var command = connection.CreateCommand();
        command.CommandText = $@"INSERT INTO [ServiceTickets] ([ServiceTicketID], [Title], [StatusValue], [Opened]) 
                                VALUES (@ServiceTicketID, @Title, @StatusValue, @Opened)";

        DbParameter parameter = null;
        parameter = command.CreateParameter();
        parameter.DbType = DbType.Guid;
        parameter.ParameterName = "@ServiceTicketID";
        parameter.Value = Guid.NewGuid();
        command.Parameters.Add(parameter);

        parameter = command.CreateParameter();
        parameter.DbType = DbType.String;
        parameter.ParameterName = "@Title";
        parameter.Value = $"Title - {Guid.NewGuid().ToString()}";
        command.Parameters.Add(parameter);

        parameter = command.CreateParameter();
        parameter.DbType = DbType.Int32;
        parameter.ParameterName = "@StatusValue";
        parameter.Value = new Random().Next(0, 10);
        command.Parameters.Add(parameter);

        parameter = command.CreateParameter();
        parameter.DbType = DbType.DateTime;
        parameter.ParameterName = "@Opened";
        parameter.Value = DateTime.Now;
        command.Parameters.Add(parameter);

        try
        {
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
    }

    public static void Agent_ApplyChangedFailed(object sender, ApplyChangesFailedArgs e)
    {
        e.Action = ConflictResolution.ClientWins;     
    }


}