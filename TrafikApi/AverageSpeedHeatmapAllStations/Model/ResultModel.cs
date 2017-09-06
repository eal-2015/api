using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AverageSpeedHeatmapAllStations.Model
{
    public class ResultModel
    {
        public int areaCode { get; set; }
        public int measurement { get; set; }
        public string name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
