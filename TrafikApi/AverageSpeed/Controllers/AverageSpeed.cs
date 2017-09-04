using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using AverageSpeed.Model;
using ModelHelper;

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
        public async Task<JsonResult> GetMeasurementsBetweenDates(DateTime from, DateTime to, string station)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ResultModel result = new ResultModel();
            try
            {
                IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Trafik_DB", "Measurements");

                try
                {
                    var output = await collection.Aggregate().
                            Match(x => x.dateTime > from && x.dateTime < to && x.stationName == station).
                            Group(r => new
                            {
                                Key = r.stationName
                            },
                            g => new
                            {
                                Key = g.Key,
                                avgValue = g.Average(x => x.speed)
                            }).Project(r => new Result()
                            {
                                stationName = r.Key.Key,
                                avgSpeed = r.avgValue
                                 
                            }).ToListAsync().ConfigureAwait(false);

                    if (output.Count() > 0)
                    {
                        IMongoCollection<Station> stations = conn.ConnectToStation("Trafik_DB", "Stations");
                        var output2 = stations.Find(Builders<Station>.Filter.Text(station)).ToList();
                        if (output2.Count > 0)
                        {
                            result.latitude = output2[0].latitude;
                            result.longitude = output2[0].longitude;
                        }
                        result.name = output[0].stationName;
                        result.measurement = output[0].avgSpeed;
                    }
                }
                catch (Exception e)
                {
                    System.IO.File.WriteAllText("DatabaseError.txt", e.Message);
                }
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText("Error.txt", e.Message);
            }
            watch.Stop();
            
            System.IO.File.WriteAllText("Elapsed.txt", watch.Elapsed.TotalSeconds.ToString());
            return Json(result);
        }
        [BsonIgnoreExtraElements]
        private class Result
        {
            public Result()
            {

            }
            [BsonRepresentation(BsonType.ObjectId)]
            public ObjectId _id { get; set; }
            [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
            public double avgSpeed { get; set; }
            [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
            public double lng { get; set; }
            [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
            public double lat { get; set; }
            [BsonRepresentation(BsonType.String, AllowTruncation = true)]
            public string stationName { get; set; }
        }
    }
}
