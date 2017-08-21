using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrafikApi.Models;
using MongoDB.Driver;
using TrafikApi.Utility;
using System.IO;

namespace TrafikApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SpeedController : Controller
    {
        Mongo conn = new Mongo();

        // GET: api/speed/Measurements?from="2017-05-01 18:00:12"&to="2017-05-01 18:00:12"&carType=2&lane=1
        [HttpGet]
        [ActionName("Measurements")]
        public JsonResult HowManyMeasurements(string from, string to, int carType, int lane)
        {
            return Json(new Json().CallPythonInCSharp("HowManyMeasurements.py", "'" + from + "' '" + to + "' '" + carType + "' '" + lane + "'"));
        }
        [HttpGet]
        [ActionName("TestJson")]
        public int TestJsonMethod()
        {
            return int.Parse(new Json().CallPythonInCSharp("test.py", ""));
        }
    }
}
