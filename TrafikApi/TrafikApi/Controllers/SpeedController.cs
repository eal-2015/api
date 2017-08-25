using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrafikApi.Models;
using MongoDB.Driver;
using TrafikApi.Utility;
using System.IO;
using Microsoft.AspNetCore.Cors;

namespace TrafikApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsAllowAllFix")]
    public class SpeedController : Controller
    {
        Mongo conn = new Mongo();

        // GET: http://adm-trafik-01.odknet.dk:1000/api/speed/Measurements?from=2017-07-18%2018:50:12&to=2017-07-20%2019:50:12&carType=2&lane=2
        [HttpGet]
        [ActionName("Measurements")]
        public JsonResult HowManyMeasurements(string from, string to, int carType, int lane)
        {
            return Json(new Json().CallPythonInCSharp("HowManyMeasurements.py", "'" + from + "' '" + to + "' '" + carType + "' '" + lane + "'"));
        }
        // GET: http://adm-trafik-01.odknet.dk:1000/api/speed/GetMax?from=2017-07-18%2018:50:12&to=2017-07-20%2019:50:12&carType=2&lane=2
        [HttpGet]
        [ActionName("GetMax")]
        public JsonResult GetMax(string from, string to, int carType, int lane)
        {
            return Json(new Json().CallPythonInCSharp("GetMax.py", "'" + from + "' '" + to + "' '" + carType + "' '" + lane + "'"));
        }
        // GET: http://adm-trafik-01.odknet.dk:1000/api/speed/GetMin?from=2017-07-18%2018:50:12&to=2017-07-20%2019:50:12&carType=2&lane=2
        [HttpGet]
        [ActionName("GetMin")]
        public JsonResult GetMin(string from, string to, int carType, int lane)
        {
            return Json(new Json().CallPythonInCSharp("GetMin.py", "'" + from + "' '" + to + "' '" + carType + "' '" + lane + "'"));
        }
        // GET: http://adm-trafik-01.odknet.dk:1000/api/speed/MeasureAvgSpeed?from=2017-07-18%2018:50:12&to=2017-07-20%2019:50:12&carType=2&lane=2
        [HttpGet]
        [ActionName("MeasureAvgSpeed")]
        public JsonResult MeasureAvgSpeed(string from, string to, int carType, int lane)
        {
            return Json(new Json().CallPythonInCSharp("MeasureAvgSpeed.py", "'" + from + "' '" + to + "' '" + carType + "' '" + lane + "'"));
        }

        // GET: http://adm-trafik-01.odknet.dk:1000/api/speed/MeasureAvgSpeed?from=2017-07-18%2018:50:12&to=2017-07-20%2019:50:12&carType=2&lane=2
        [HttpGet]
        [ActionName("AverageOfDays")]
        public JsonResult AverageOfDays(string from, string to, int carType, int lane)
        {
            return Json(new Json().CallPythonInCSharp("AverageOfDays.py", "'" + from + "' '" + to + "' '" + carType + "' '" + lane + "'"));
        }



        [HttpGet]
        [ActionName("TestJson")]
        public int TestJsonMethod()
        {
            return int.Parse(new Json().CallPythonInCSharp("test.py", ""));
        }
    }
}
