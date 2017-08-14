using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TrafikApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrafikApi.Controllers
{
    [Route("api/[controller]")]
    public class ReaderController : Controller
    {
        IMongoCollection<Station> collection;

        // GET: api/reader
        [HttpGet]
        public JsonResult Get()
        {
            Connect("mongodb://localhost:27017", "Embedded_test", "Stations"); //has to be changed so it gets parsed in the strings it needs for this
            var filt = Builders<Station>.Filter.Where(m => m.name != null);
            List<Station> stations = collection.Find(filt).ToList();

            //return Json(stations);

            /*
             * Could also just return the list of stations as shown above.
             * But if our DB is embedded then you also get all the measurements of each stations
             */

            string[][] temp = new string[stations.Count][];

            for (int i = 0; i < stations.Count; i++)
            {
                temp[i] = new string[] { stations[i].name, stations[i].areacode.ToString() };
            }
            return Json(temp);
        }

        public void Connect(string connectionString, string databaseName, string collectionName) //Should be set a the Contructor if it gets its own class
        {
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName); // is made if not already there

            collection = database.GetCollection<Station>(collectionName); // is made if not already there
        }
        
    }
}
