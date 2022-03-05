using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorDataApp.Model
{
    public class DataPoint
    {
        public string? SensorId { get; set; }

        public double Value { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
