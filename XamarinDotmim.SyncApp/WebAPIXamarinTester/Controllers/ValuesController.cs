using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dotmim.Sync.SqlServer;
using Dotmim.Sync;
using System.Data.Sql;
using Dotmim.Sync.Web.Server;
using Dotmim.Sync.Enumerations;

namespace WebAPIXamarinTester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private WebProxyServerProvider proxyServerProvider;

        public ValuesController(WebProxyServerProvider proxy)
        {
            proxyServerProvider = proxy;
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            return "Get values called!";
        }

        // POST api/values
        [HttpPost]
        public async Task Post()//[FromBody] string value
        {
            await proxyServerProvider.HandleRequestAsync(this.HttpContext);
        }

    }
}
