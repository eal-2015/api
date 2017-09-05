using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AverageSpeed.Model
{
    public class ResultModel
    {
        public string name { get; set; }
        public double measurement { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
