using System.IO;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dotmim.Sync.SampleWebServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //Server Remote Provider setup for sync config
        //Server
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

           
            //services.AddMvc();

            services.AddMemoryCache();

            var connectionString = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];


            /*Config values on Server Side here - Overrides Client values*/
            services.AddSyncServer<SqlSyncProvider>(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=advworks.sqlite;Integrated Security=true;",
               c =>

               /*services.AddSyncServer<SqlSyncProvider>(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks;Integrated Security=true;",
                   c =>*/

              /*services.AddSyncServer<SqliteSyncProvider>(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks;Integrated Security=true;",
              c =>*/

               /*services.AddSyncServer<SqliteSyncProvider>(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=advworks.sqlite;Integrated Security=true;",
              c =>*/

              /*services.AddSyncServer<SqlSyncProvider>(connectionString,
                   c =>*/
              {
                   var tables = new string[] {"ProductCategory",
                            "ProductModel",
                            "Product", 
                            "Address", "Customer", "CustomerAddress",
                            "SalesOrderHeader", "SalesOrderDetail" };
                    c.Add(tables);
                    c.ScopeInfoTableName = "tscopeinfo";
                    c.SerializationFormat = Enumerations.SerializationFormat.Binary;
                    c.StoredProceduresPrefix = "s";
                    c.StoredProceduresSuffix = "";
                    c.TrackingTablesPrefix = "t";
                    c.TrackingTablesSuffix = "";


                }, options =>
                {
                    options.BatchDirectory = Path.Combine(SyncOptions.GetDefaultUserBatchDiretory(), "server");
                    options.BatchSize = 100;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
