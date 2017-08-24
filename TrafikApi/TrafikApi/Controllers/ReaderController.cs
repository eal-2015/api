using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TrafikApi.Models;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrafikApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsAllowAllFix")]
    public class ReaderController : Controller
    {
        Mongo conn = new Mongo();
        // GET: http://adm-trafik-01.odknet.dk:1000/api/Reader/GetAllStations
        [HttpGet]
        [ActionName("GetAllStations")]
        public JsonResult GetAllStations() //Have to be changed so it get parsed in the string for connection
        {
            IMongoCollection<Station> collection = conn.ConnectToStation("Trafik_DB", "Stations");
            var filt = Builders<Station>.Filter.Where(m => m.name != null);

            return Json(collection.Find(filt).ToList());
        }
        // GET: http://adm-trafik-01.odknet.dk:1000/api/reader/GetStation?name=Falen km 1,569
        [HttpGet]
        [ActionName("GetStation")]
        public JsonResult GetStation(string name) //Have to be changed so it get parsed in the string for connection
        {
            IMongoCollection<Station> collection = conn.ConnectToStation("Trafik_DB", "Stations");
            var filt = Builders<Station>.Filter.Where(m => m.name == name);

            return Json(collection.Find(filt).ToList());
        }
        // GET: http://adm-trafik-01.odknet.dk:1000/api/reader/GetAllMeasurementOnStation?station=Falen km 1,569
        [HttpGet]
        [ActionName("GetAllMeasurementOnStation")]
        public JsonResult GetAllMeasurementOnStation(string station) //Have to be changed so it get parsed in the string for connection
        {
            IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Trafik_DB", "Measurements");
            var filt = Builders<Measurement>.Filter.Where(m => m.stationName == station);

            return Json(collection.Find(filt).ToList());
        }
        // GET: http://adm-trafik-01.odknet.dk:1000/api/reader/GetMeasurementsBetweenDates?from=2017-07-08%2000:00:00&to=2017-07-10%2000:00:00
        [HttpGet]
        [ActionName("GetMeasurementsBetweenDates")]
        public JsonResult GetMeasurementsBetweenDates(DateTime from, DateTime to)
        {
            //TODO Kald databasen og få elementer ud og send dem over til python
            List<string> input = new List<string>();
            IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Trafik_DB", "Measurements");

            var result = collection.Find(Builders<Measurement>.Filter.Where(x => x.dateTime > from && x.dateTime < to)).ToList();

            return Json(result);
        }
    }
}
