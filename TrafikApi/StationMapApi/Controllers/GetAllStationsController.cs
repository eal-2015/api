using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using ModelHelper;
using MongoDB.Driver;

namespace StationMapApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsAllowAllFix")]
    public class GetAllStationsController : Controller
    {
        Mongo conn = new Mongo();
        // GET: http://adm-trafik-01.odknet.dk:2002/api/GetAllStations/Stations
        [HttpGet]
        [ActionName("Stations")]
        public JsonResult GetAllStations() //Have to be changed so it get parsed in the string for connection
        {
            IMongoCollection<Station> collection = conn.ConnectToStation("Trafik_DB", "Stations");
            var filt = Builders<Station>.Filter.Where(m => m.name != null);

            return Json(collection.Find(filt).ToList());
        }
    }
}
