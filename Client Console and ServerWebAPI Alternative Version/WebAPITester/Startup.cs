using Dotmim.Sync.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPITester
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMemoryCache();

            var connString = Configuration["Data:ConnectionString"];

           /* services.AddSyncServer<SqlSyncProvider>(connString, configuration =>
            {
                configuration.Tables = new string[] { "ProductCategory",
                        "ProductModel",
                        "Product",
                        "Address", "Customer", "CustomerAddress",
                        "SalesOrderHeader", "SalesOrderDetail" };
            });*/

            services.AddSyncServer<SqlSyncProvider>(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdvetureWorks;Integrated Security=true;",
             c =>
             {
                 var tables = new string[] {"ProductCategory",
                            "ProductModel",
                            "Product",
                            "Address", "Customer", "CustomerAddress",
                            "SalesOrderHeader", "SalesOrderDetail" };
                 c.Add(tables);
                 c.ScopeInfoTableName = "tscopeinfo";
                 c.SerializationFormat = Dotmim.Sync.Enumerations.SerializationFormat.Binary;
                 c.StoredProceduresPrefix = "s";
                 c.StoredProceduresSuffix = "";
                 c.TrackingTablesPrefix = "t";
                 c.TrackingTablesSuffix = "";
             });
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IHostingEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
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
