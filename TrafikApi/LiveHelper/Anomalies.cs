using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveHelper
{
    [BsonIgnoreExtraElements]
    public class Anomalies
    {
        public int areacode;
        public float anomaly_likelihood;
        public float anomaly_score;
        public int value;
        public DateTime date;
    }
}
