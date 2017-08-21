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
            string result = "";
            try
            {
                Json json = new Json();

                result = json.CallPythonInCSharp("test.py", "'" + from + "' '" + to + "' '" + carType + "' '" + lane + "'");
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText("test3.txt", e.InnerException.Message);
            }
            return Json(result);
        }
        [HttpGet]
        [ActionName("TestJson")]
        public int TestJsonMethod()
        {
            int output = 0;
            try
            {
                Json json = new Json();

                output = int.Parse(json.CallPythonInCSharp("test.py", ""));
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText("test3.txt", e.InnerException.Message);
            }
            return output;
        }
    }
}
