﻿using System;
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
        Mongo conn;
        // GET: api/reader
        [HttpGet]
        public JsonResult GetAllStations() //Have to be changed so it get parsed in the string for connection
        {
            IMongoCollection<Station> collection = conn.ConnectToStation("Flat_test", "Stations");
            var filt = Builders<Station>.Filter.Where(m => m.name != null);
            
            return Json(collection.Find(filt).ToList());
        }
        [HttpGet]
        public JsonResult GetStation(string name) //Have to be changed so it get parsed in the string for connection
        {
            IMongoCollection<Station> collection = conn.ConnectToStation("Flat_test", "Stations");
            var filt = Builders<Station>.Filter.Where(m => m.name == name);

            return Json(collection.Find(filt).ToList());
        }
        [HttpGet]
        public JsonResult GetAllMeasurementOnStation(string station) //Have to be changed so it get parsed in the string for connection
        {
            IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Flat_test", "Measurement");
            var filt = Builders<Measurement>.Filter.Where(m => m.stationName == station);

            return Json(collection.Find(filt).ToList());
        }
        [HttpGet]
        public JsonResult GetMeasurementsBetweenDates(DateTime from, DateTime to)
        {
            //TODO Kald databasen og få elementer ud og send dem over til python
            List<string> input = new List<string>();
            IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Flat_test", "Measurement");
            
            var result = collection.Find(Builders<Measurement>.Filter.Where(x => x.dateTime > from && x.dateTime < to)).ToList();

            return Json(result);
        }
    }
}
