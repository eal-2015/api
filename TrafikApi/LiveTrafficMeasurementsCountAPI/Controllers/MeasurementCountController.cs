using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using LiveHelper;

namespace LiveTrafficMeasurementsCountAPI.Controllers
{
    // TO DO
    [EnableCors("AllowAll")]
    [Route("api/[controller]/[action]")]
    public class MeasurementCountController : Controller
    {
        Mongo conn = new Mongo();

        [HttpGet]
        [ActionName("GetMeasurements")]
        // GET: http://localhost:57719/api/MeasurementCount/GetMeasurements?from=2017-08-14%2002:00:00&to=2017-09-15%2002:00:00&stationid=46102360
        // GET: http://adm-trafik-01.odknet.dk:2008/api/MeasurementCount/GetMeasurements?from=2017-08-14%2009:50:40&to=2017-09-15&stationid=46102360
        public JsonResult GetMeasurements(DateTime from, DateTime to, int stationid)
        {
            IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Trafik_DB", "LiveMeasurements");
            var filt = Builders<Measurement>.Filter.Where(x => x.stationid == stationid) & Builders<Measurement>.Filter.Where(x => x.datetime >= from.ToUniversalTime() && x.datetime <= to.ToUniversalTime());

            var CountResult = collection.Find(filt).Count();

            return Json(CountResult);
        }
      
    }
}


