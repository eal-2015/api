using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrafikApi.Models
{
    [BsonIgnoreExtraElements]
    public class Station
    {
        public int areacode { get; set; }
        public string name { get; set; }
        public DateTime installed { get; set; }
        public string equipmentType { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

        public Station()
        {

        }
        public Station(int areacode, string name, DateTime installed, string equipmentType, double latitude, double longitude)
        {
            this.areacode = areacode;
            this.name = name;
            this.installed = installed;
            this.equipmentType = equipmentType;
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}
