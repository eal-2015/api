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
        public int lane { get; set; }
        public int speed { get; set; }
        public int length { get; set; }
        public int carType { get; set; }
        public int gap { get; set; }
        public int wrongDir { get; set; }
        public int display { get; set; }
        public int flash { get; set; }
        public string stationName { get; set; }

        public Measurement(DateTime dateTime, int lane, int speed, int length, int carType, int gap, int wrongDir, int display, int flash, string stationName)
        {
            this.dateTime = dateTime;
            this.lane = lane;
            this.speed = speed;
            this.length = length;
            this.carType = carType;
            this.gap = gap;
            this.wrongDir = wrongDir;
            this.display = display;
            this.flash = flash;
            this.stationName = stationName;
        }
    }
}
