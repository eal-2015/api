using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrafikApi.Models
{
    [BsonIgnoreExtraElements]
    public class Measurement
    {
        public DateTime dateTime { get; set; }
        public string lane { get; set; }
        public string speed { get; set; }
        public string length { get; set; }
        public string type { get; set; }
        public string gap { get; set; }
        public string wrongDir { get; set; }
        public string display { get; set; }
        public string flash { get; set; }
        public string stationName { get; set; }

        public Measurement(DateTime dateTime, string lane, string speed, string length, string type, string gap, string wrongDir, string display, string flash, string stationName)
        {
            this.dateTime = dateTime;
            this.lane = lane;
            this.speed = speed;
            this.length = length;
            this.type = type;
            this.gap = gap;
            this.wrongDir = wrongDir;
            this.display = display;
            this.flash = flash;
            this.stationName = stationName;
        }
    }
}
