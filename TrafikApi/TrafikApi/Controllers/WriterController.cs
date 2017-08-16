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
        Mongo conn;
        // POST api/values
        [HttpGet]
        public void InsertStation(string name, int areacode)
        {
            IMongoCollection<Station> collection = conn.ConnectToStation("Flat_test", "Stations");
            collection.InsertOne(new Station(name, areacode));
        }
        // POST api/values
        [HttpPost]
        public void InsertMeasurement(DateTime dateTime, string lane, string speed, string length, string type, string gap, string wrongDir, string display, string flash, string stationName)
        {
            IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Flat_test", "Stations");
            collection.InsertOne(new Measurement(dateTime, lane, speed, length, type, gap, wrongDir, display, flash, stationName));
        }
    }
}
