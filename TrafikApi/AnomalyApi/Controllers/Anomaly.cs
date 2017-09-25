using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using LiveHelper;
using System.Collections.Generic;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace AnomalyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsAllowAllFix")]
    public class Anomaly : Controller
    {
        /// <summary>
        /// Example http://adm-trafik-01.odknet.dk:2011/api/Anomaly/GetAnomalies?from=2016-02-02%2000:00:00&to=2019-09-09%2000:00:00&areacode=46102360
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="areacode"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetAnomalies")]
        public JsonResult GetAnomalies(DateTime from, DateTime to, int areacode)
        {
            List<Anomalies> result = new List<Anomalies>();
            try
            {
                Mongo mongo = new Mongo();
                IMongoCollection<Anomalies> collection = mongo.ConnectToAnomaly("Traffic_Anomalies", "Anomalies");
                result = collection.Find(Builders<Anomalies>.Filter.Where(x => x.date < to && x.date > from && x.areacode == areacode)).ToList();
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText("Error.txt", e.Message);
            }
            return Json(result);
        }
    }
}
