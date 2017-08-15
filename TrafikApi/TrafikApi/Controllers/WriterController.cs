using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrafikApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrafikApi.Controllers
{
    [Route("api/[controller]")]
    public class WriterController : Controller
    {
        IMongoCollection<Station> Stationcollection;
        IMongoCollection<Measurement> Measurementcollection;
        // POST api/values
        [HttpGet]
        public bool InsertStation(string name, int areacode)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("Flat_test"); // is made if not already there

            Stationcollection = database.GetCollection<Station>("Stations"); // is made if not already there

            Stationcollection.InsertOne(new Station(name, areacode));
            return true;
        }
        // POST api/values
        [HttpPost]
        public void InsertMeasurement(string dateTime, string lane, string speed, string length, string type, string gap, string wrongDir, string display, string flash, string stationName)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("Flat_test"); // is made if not already there

            Measurementcollection = database.GetCollection<Measurement>("Stations"); // is made if not already there

            Measurementcollection.InsertOne(new Measurement(dateTime, lane, speed, length, type, gap, wrongDir, display, flash, stationName));
        }
    }
}
