using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotmim.Sync.Enumerations;
using Dotmim.Sync.Web.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
//Server
namespace Dotmim.Sync.SampleWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
       // private readonly WebProxyServerProvider webProxyServer;
        private WebProxyServerProvider webProxyServer;

        // Injected thanks to Dependency Injection
        public SyncController(WebProxyServerProvider proxy)
        {
            webProxyServer = proxy;
        }

        [HttpPost]
        public async Task Post()
        {
            // Get the underline local provider
            var provider = webProxyServer.GetLocalProvider(this.HttpContext);

            //provider.SetConfiguration(c => c.Filters.Add("Customer", "CustomerID"));

           /* provider.InterceptApplyChangesFailed(e =>
            {
                //Resolve conflict by specifying merged row
                if (e.Conflict.RemoteRow.Table.TableName == "ProductModel")
                {
                    e.Action = ConflictResolution.MergeRow;
                    e.FinalRow["ProductModelID"] = 20;
                }
                else
                {
                    e.Action = ConflictResolution.ServerWins;
                }
            });*/

            await webProxyServer.HandleRequestAsync(this.HttpContext);
        }
    }
}
