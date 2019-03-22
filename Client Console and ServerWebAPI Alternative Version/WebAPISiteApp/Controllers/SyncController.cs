using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotmim.Sync.Web.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dotmim.Sync.SqlServer;
using Dotmim.Sync;
using System.Data.Sql;

namespace WebAPISiteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private WebProxyServerProvider webProxyServer;

        public SyncController(WebProxyServerProvider proxy)
        {
            webProxyServer = proxy;
        }

        // POST: api/Sync
        [HttpPost]
        public async Task Post()
        {    
            await webProxyServer.HandleRequestAsync(this.HttpContext);
        }  
    }
}
