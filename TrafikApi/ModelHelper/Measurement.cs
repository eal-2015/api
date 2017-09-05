using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ModelHelper
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
        public int areaCode { get; set; }

        public Measurement(DateTime dateTime, int lane, int speed, int length, int carType, int gap, int wrongDir, int display, int flash, int areaCode)
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
            this.areaCode = areaCode;
        }

        public string ToPython()
        {
            return dateTime.ToString() + "," + lane + "," + speed + "," + length + "," + carType + "," + gap + "," + wrongDir + "," + display + "," + flash + ";";
        }
    }
}
