using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using ModelHelper;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarTypeHeatMapApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsAllowAllFix")]
    public class GetAllStationsController : Controller
    {
        Mongo conn = new Mongo();
        // GET: http://adm-trafik-01.odknet.dk:2004/api/GetAllStations/Stations
        [HttpGet]
        [ActionName("Stations")]
        public JsonResult GetAllStations() //Have to be changed so it get parsed in the string for connection
        {
            IMongoCollection<Station> collection = conn.ConnectToStation("Trafik_DB", "Stations");
            var filt = Builders<Station>.Filter.Where(m => m.name != null);

            List<Station> stations = collection.Find(filt).ToList();

            List<Result> result = new List<Result>();
            foreach (Station item in stations)
            {
                result.Add(new Result(item.name, item.areacode));
            }
            return Json(result);
        }
    }
    class Result
    {
        public string name { get; set; }
        public int areacode { get; set; }
        public Result(string name, int areacode)
        {
            this.name = name;
            this.areacode = areacode;
        }
    }
}

