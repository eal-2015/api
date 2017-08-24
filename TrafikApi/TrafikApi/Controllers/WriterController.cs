using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrafikApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrafikApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsAllowAllFix")]
    public class WriterController : Controller
    {
        Mongo conn = new Mongo();
        // POST api/values
        [HttpGet]
        [ActionName("InsertStation")]
        public void InsertStation(int areacode, string name, DateTime installed, string equipmentType, double latitude, double longitude)
        {
            IMongoCollection<Station> collection = conn.ConnectToStation("Trafik_DB", "Stations");
            collection.InsertOne(new Station(areacode, name, installed, equipmentType, latitude, longitude));
        }
        // POST api/values
        [HttpPost]
        [ActionName("InsertMeasurement")]
        public void InsertMeasurement(DateTime dateTime, string lane, string speed, string length, string type, string gap, string wrongDir, string display, string flash, string stationName)
        {
            IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Trafik_DB", "Measurements");
            collection.InsertOne(new Measurement(dateTime, int.Parse(lane), int.Parse(speed), int.Parse(length), int.Parse(type), int.Parse(gap), int.Parse(wrongDir), int.Parse(display), int.Parse(flash), stationName));
        }
        [HttpGet]
        [ActionName("SaveMeasurements")]
        public void SaveMeasurements(List<Measurement> inpmeasurements)
        {
            //Reads from a file and set in all measurements from the file
            /*var Lines = File.ReadLines("dataset.csv").Select(a => a.Split('\t'));
            List<Measurement> measurements = inpmeasurements;
            int count = 0;
            foreach (string[] item2 in Lines)
            {
                if (count != 0)
                {
                    string test = "";
                    foreach (var item3 in item2)
                    {
                        test += item3;
                    }
                    string[] item = test.ToString().Split(';');
                    string ab = item[0];
                    item[7] = item[7][item[7].Length - 1].ToString();
                    database.GetCollection<Measurement>("Measurement").InsertOne(new Measurement(double.Parse(item[0]), double.Parse(item[1]), DateTime.Parse(item[2]), int.Parse(item[3]), int.Parse(item[4]), int.Parse(item[5]), int.Parse(item[6]), int.Parse(item[7]))); //check efter x antal og hvis for stor skriv til db og clear liste, kunne være en læsning.
                }
                count++;
            }
            ;*/
            IMongoCollection<Measurement> collection = conn.ConnectToMeasurement("Trafik_DB", "Measurements");
            foreach (var item in inpmeasurements)
            {
                collection.InsertOne(item);
            }
        }
    }
}
