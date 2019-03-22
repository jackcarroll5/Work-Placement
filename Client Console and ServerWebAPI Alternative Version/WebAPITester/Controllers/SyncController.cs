using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotmim.Sync.Web.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dotmim.Sync.Enumerations;

namespace WebAPITester.Controllers
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

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] string value)
        {
            await webProxyServer.HandleRequestAsync(this.HttpContext);
        }   
    }
}
