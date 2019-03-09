using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotmim.Sync.Web.Server;
using Dotmim.Sync.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPISiteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncingController : ControllerBase
    {
        private WebProxyServerProvider webProxyServer;

        public SyncingController(WebProxyServerProvider proxy)
        {
            webProxyServer = proxy;
        }

        // POST: api/Syncing
        [HttpPost]
        public async Task Post()
        {
            var provider = webProxyServer.GetLocalProvider(this.HttpContext);


            await webProxyServer.HandleRequestAsync(this.HttpContext);
        }  
    }
}
