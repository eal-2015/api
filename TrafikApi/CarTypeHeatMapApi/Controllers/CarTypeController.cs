using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelHelper;
using MongoDB.Driver;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarTypeHeatMapApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsAllowAllFix")]
    public class CarTypeController : Controller
    {
        Stopwatch timer = new Stopwatch();
        
        Mongo conn = new Mongo();

        // GET: http://adm-trafik-01.odknet.dk:2004/api/CarType/GetCarTypes?from=2017-02-02%2000:00:00&to=2017-05-05%2000:00:00&station=Anderupvej
        [HttpGet]
        [ActionName("GetCarTypes")]
        public JsonResult GetCarTypes(DateTime from, DateTime to, int areaCode) //Have to be changed so it get parsed in the string for connection
        {
            timer.Start();
            IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Trafik_DB", "Measurements");
            var filt = Builders<Measurement>.Filter.Where(x => x.areaCode == areaCode) & Builders<Measurement>.Filter.Where(x => x.dateTime > from && x.dateTime < to);
            /*
             * Should add a filter to sort and count the number of each type of car
             * so it is the DB and not the script that has to do the work
             * and so we don't have to return the objects, but just the amount of each match
             */
            var result = collection.Find(filt).ToList();

            Dictionary<int, int> types = new Dictionary<int, int>();

            //List<int> types = new List<int>();
            
            foreach (var item in result)
            {
                if (types.ContainsKey(item.carType) == false)
                {
                    types.Add(item.carType, 0);
                }
                types[item.carType]++;
            }
            
            int[] sorted = new int[types.Count];
            foreach (var item in types)
            {
                sorted[item.Key] = item.Value;
            }

            return Json(sorted);
        }
    }
}
