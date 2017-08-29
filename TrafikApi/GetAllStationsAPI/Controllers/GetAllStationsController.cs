using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MongoDB.Driver;
using ModelHelper;
using ModelHelper.Utility;

namespace GetAllStationsAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsAllowAllFix")]
    public class GetAllStationsAPI : Controller
    {
        Mongo conn = new Mongo();
        // GET: http://adm-trafik-01.odknet.dk:2002/api/GetAllStationsAPI/GetAllStations
        [HttpGet]
        [ActionName("GetAllStations")]
        public JsonResult GetAllStations() //Have to be changed so it get parsed in the string for connection
        {
            IMongoCollection<Station> collection = conn.ConnectToStation("Trafik_DB", "Stations");
            var filt = Builders<Station>.Filter.Where(m => m.name != null);

            return Json(collection.Find(filt).ToList());
        }
    }
}