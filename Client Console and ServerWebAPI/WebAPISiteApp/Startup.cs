﻿using System.IO;
using Dotmim.Sync;
using Dotmim.Sync.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Dotmim.Sync.Web.Server;
using System.Data.Sql;
using Dotmim.Sync.Enumerations;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WebAPISiteApp
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

            services.AddMvc();

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            //var connectionString = Configuration["Data:ConnectionString"];

            var connectionString = Configuration["Data:(localdb)\\MSSQLLocalDB;Initial Catalog=AdventureWorks;Integrated Security=true;"];

            /*services.AddSyncServer<SqlSyncProvider>(connectionString, configuration =>
            {
                configuration.Tables = new string[] { "ProductCategory","Address",
                                                     "Product","ProductModel",
                                                     "Customer", "CustomerAddress",
                                                      "SalesOrderDetail","SalesOrderHeader" };
            });*/

            /*services.AddSyncServer<SqlSyncProvider>(connectionString, c => {
                var tables = new string[] {"ProductCategory",
                            "ProductModel",
                            "Product",
                            "Address", "Customer", "CustomerAddress",
                            "SalesOrderHeader", "SalesOrderDetail" };
                c.Add(tables);
                c.ScopeInfoTableName = "tscopeinfo";
                c.SerializationFormat = SerializationFormat.Binary;
                c.StoredProceduresPrefix = "s";
                c.StoredProceduresSuffix = "";
                c.TrackingTablesPrefix = "t";
                c.TrackingTablesSuffix = "";


            }, options =>
            {
                options.BatchDirectory = Path.Combine(SyncOptions.GetDefaultUserBatchDiretory(), "server");
                options.BatchSize = 100;
            });*/

            services.AddSyncServer<SqlSyncProvider>(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks;Integrated Security=true;", c => {
                  var tables = new string[] {"ProductCategory",
                            "ProductModel",
                            "Product",
                            "Address", "Customer", "CustomerAddress",
                            "SalesOrderHeader", "SalesOrderDetail" };
                  c.Add(tables);
                  c.ScopeInfoTableName = "tscopeinfo";
                  c.SerializationFormat = SerializationFormat.Binary;
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