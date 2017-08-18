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
        public int HowManyMeasurements(string from, string to, int carType, int lane)
        {
            int output = 0;
            try
            {
            IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Trafik_DB", "Measurements");
            List<Measurement> result = collection.Find(Builders<Measurement>.Filter.Where(x => x.dateTime > DateTime.Parse(from) && x.dateTime < DateTime.Parse(to))).ToList();

            Json json = new Json();

            string arguments = "'";

            foreach (var item in result)
            {
                arguments += item.ToPython();
            }
                output = int.Parse(json.CallPythonInCSharp(@"E:\Scripts\HowManyMeasurements.py", @"C:\Program Files\Python36", arguments)); 
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText("test3.txt", e.Message);
            }
            return output;
        }
    }
}
