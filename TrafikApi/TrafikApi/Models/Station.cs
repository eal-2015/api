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
        public string name { get; set; }
        public int areacode { get; set; }

        public Station(string name, int areacode)
        {
            this.name = name;
            this.areacode = areacode;
        }
    }
}
