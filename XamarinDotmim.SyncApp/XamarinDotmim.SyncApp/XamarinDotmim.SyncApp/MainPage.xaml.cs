using Dotmim.Sync;
using Dotmim.Sync.Web.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using Xamarin.Forms;
using Dotmim.Sync.Sqlite;
using System.IO;
using System.Reflection;

namespace XamarinDotmim.SyncApp
{
    public partial class MainPage : ContentPage
    {

        public string[] allTables = new string[] {"ProductCategory",
                                                     "Product","ProductModel", "Address",
                                                     "Customer", "CustomerAddress",
                                                    "SalesOrderHeader", "SalesOrderDetail"};
        public MainPage()
        {
            InitializeComponent();

            //TestWebAPIASync().GetAwaiter().GetResult();
        }



        private async Task TestWebAPIASync()
        {
            //MainPage p = new MainPage();

            //string cName = "This PC\\Galaxy Tab A (2016)\\Card\\Android\\media\\adventworks.sqlite";
            var cName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ModelTest.sqlite");
            var sqliteSync = new SqliteSyncProvider(cName); ///data/data/com.companyname/files/.local/share/adventworks.sqlite - Tablet (Originally)
                                                            ///data/user/0/com.companyname/files/.local/share/adventworks.sqlite - Phone

            var proxyServerProvider = new WebProxyClientProvider(new Uri("http://10.14.17.198:53065/api/values"));

            var agent = new SyncAgent(sqliteSync,proxyServerProvider);

            agent.SetConfiguration(s =>
            
            {
                s.ScopeInfoTableName = "tscopeinfo";
                s.SerializationFormat = Dotmim.Sync.Enumerations.SerializationFormat.Binary;
                s.StoredProceduresPrefix = "s";
                s.StoredProceduresSuffix = "";
                s.TrackingTablesPrefix = "t";
                s.TrackingTablesSuffix = "";
            });

            var progressS = new Progress<ProgressArgs>(pa => Context.Text = $"{pa.Context.SessionId} \t\n- {pa.Context.SyncStage}\t\n {pa.Message}");
            //var progressC = new Progress<ProgressArgs>(pa => Context.Text = $"{pa.Context.SessionId} \t\n- {pa.Context.SyncStage}\t\n {pa.Message}");


            await DisplayAlert("Begin Sync","Web Sync Starting","OK");

                try
                {
                 agent.RemoteProvider.SetProgress(progressS);
               // agent.RemoteProvider.SetProgress(progressC);

                var c = await agent.SynchronizeAsync(progressS);

               // var s = await agent.SynchronizeAsync(progressC);

                Progress.Text = "Sync Progress - Agent Server\n\n" + c.ToString();

               // ProgressClient.Text = "\n\nSync Progress - Agent Client\n\n" + s.ToString();
            }

                catch(SyncException e)
                {
                 Exception.Text = "Exception Result(If it occurs):\n" + e.ToString();
                }

                catch(Exception e)
                {
                await DisplayAlert("Exception Cause", "Unknown Exception : " + e.Message, "OK");
                }

            await DisplayAlert("Restart Sync?", "Sync Ended. Tap the Start Sync button to start again or press the quit button at the top right corner of the screen", "OK");

            await DisplayAlert("Sync Finished","Finished","OK");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await TestWebAPIASync();
        }

        private void Quit_Button(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
        }

    }
}
