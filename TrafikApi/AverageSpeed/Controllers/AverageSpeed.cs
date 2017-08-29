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
            Dictionary<string, long> resultMeasurements = new Dictionary<string, long>();
            Dictionary<string, long> resultCount = new Dictionary<string, long>();
            Dictionary<string, long> result = new Dictionary<string, long>();

            try
            {
                IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Trafik_DB", "Measurements");

                var output = collection.Find(Builders<Measurement>.Filter.Text(station) & Builders<Measurement>.Filter.Where(x => x.dateTime > from && x.dateTime < to)).ToList();

                foreach (Measurement measurement in output)
                {
                    if (resultMeasurements.ContainsKey(measurement.stationName))
                    {
                        resultMeasurements[measurement.stationName] += measurement.speed;
                        resultCount[measurement.stationName] += 1;
                    }
                    else
                    {
                        resultMeasurements.Add(measurement.stationName, measurement.speed);
                        resultCount.Add(measurement.stationName, 1);
                    }
                }
                for (int i = 0; i < resultMeasurements.Count; i++)
                {
                    result.Add(resultMeasurements.ElementAt(i).Key, resultMeasurements.ElementAt(i).Value / resultCount.ElementAt(i).Value);
                }
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText("Error.txt", e.Message);
            }
            return Json(result);
        }
    }
}
