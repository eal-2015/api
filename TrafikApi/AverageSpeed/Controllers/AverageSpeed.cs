using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using AverageSpeed.Models;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;

namespace AverageSpeed.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsAllowAllFix")]
    public class AverageSpeed : Controller
    {
        Mongo conn = new Mongo();
        // GET: http://adm-trafik-01.odknet.dk:2001/api/AverageSpeed/GetMeasurementsBetweenDates?from=2017-02-02%2000:00:00&to=2017-05-05%2000:00:00&station=Anderupvej
        [HttpGet]
        [ActionName("GetMeasurementsBetweenDates")]
        public JsonResult GetMeasurementsBetweenDates(DateTime from, DateTime to, string station)
        {
            long measurements = 0;
            try
            {
                IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Trafik_DB", "Measurements");

                var output = collection.Find(Builders<Measurement>.Filter.Text(station) & Builders<Measurement>.Filter.Where(x => x.dateTime > from && x.dateTime < to)).ToList();
                
                for (int i = 0; i < output.Count; i++)
                {
                    measurements += output[i].speed;
                }
                measurements = measurements / output.Count;
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText("Error.txt", e.Message);
            }
            return Json(measurements);
        }
    }
}
