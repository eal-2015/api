using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Cors;
using ModelHelper;
using System.Collections.Generic;
using AverageSpeedHeatmapAllStations.Model;
using System.Numerics;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AverageSpeedHeatmapAllStations.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsAllowAllFix")]
    public class AverageSpeed : Controller
    {
        Mongo conn = new Mongo();
        // GET: http://adm-trafik-01.odknet.dk:2003/api/AverageSpeed/GetMeasurementsBetweenDatesAllStations?from=2017-02-02%2000:00:00&to=2017-05-05%2000:00:00
        [HttpGet]
        [ActionName("GetMeasurementsBetweenDatesAllStations")]
        public async Task<JsonResult> GetMeasurementsBetweenDatesAllStations(DateTime from, DateTime to)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            List<ResultModel> result = new List<ResultModel>();
            try
            {
                IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Trafik_DB", "Measurements");
                IMongoCollection<Station> stations = conn.ConnectToStation("Trafik_DB", "Stations");

                List<Station> result2 = stations.Find(Builders<Station>.Filter.Where(m => m.areacode != 0)).ToList();

                try
                {
                    var output = await collection.Aggregate().
                            Match(x => x.dateTime > from).
                            Match(x => x.dateTime < to).
                            Group(r => new
                            {
                                Key = r.areaCode
                            },
                            g => new
                            {
                                Key = g.Key,
                                avgValue = g.Average(x => x.speed)
                            }).Project(r => new Result()
                            {
                                avgValue = (int)r.avgValue,
                                areaCode = r.Key.Key

                            }).ToListAsync().ConfigureAwait(false);



                    for (int i = 0; i < output.Count; i++)
                    {
                        ResultModel resultModel = new ResultModel();
                        resultModel.areaCode = output[i].areaCode;
                        resultModel.measurement = (int)output[i].avgValue;
                        result.Add(resultModel);
                    }

                    foreach (var item in result2)
                    {
                        ResultModel res = result.Find(x => x.areaCode == item.areacode);
                        if (res != null)
                        {
                            res.longitude = item.longitude;
                            res.latitude = item.latitude;
                            res.name = item.name;
                        }
                        else
                        {
                            result.Add(new ResultModel { name = item.name, latitude = item.latitude, longitude = item.longitude, measurement = 0, areaCode = item.areacode });
                        }
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
            public double avgValue { get; set; }
            [BsonRepresentation(BsonType.String, AllowTruncation = true)]
            public int areaCode { get; set; }
        }
    }
}



