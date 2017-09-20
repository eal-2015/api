using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LiveHelper
{
    [BsonIgnoreExtraElements]
    public class Measurement
    {
       
        public DateTime datetime { get; set; }
        public int lane { get; set; }
        public int speed { get; set; }
        public int length { get; set; }
        public int @class { get; set; }
        public int gap { get; set; }
        public int wrong_dir { get; set; }
        public int display { get; set; }
        public int flash { get; set; }
        public int stationid { get; set; }
    }
}
