using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using ModelHelper.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StartAnalysis.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowAll")]
    public class Analysis : Controller
    {
        // GET: api/values
        [HttpGet]
        [ActionName("start")]
        public string Get()
        {
            Json json = new Json();
            return json.CallPythonInCSharp("AnomalyDataCollector", "", ModelHelper.Utility.Json.Environments.TrafficAnalyze);
        }
    }
}
