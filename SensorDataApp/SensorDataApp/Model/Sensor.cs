using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorDataApp.Model
{
    public class Sensor
    {
        public string? Id { get; set; }

        public double UpperLimit { get; set; }

        public double LowerLimit { get; set; }
    }
}
