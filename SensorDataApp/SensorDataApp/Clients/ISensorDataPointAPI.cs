using Refit;
using SensorDataApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataApp
{
    public interface ISensorDataPointAPI
    {
        [Get("/{sensorId}")]
        Task<ApiResponse<IEnumerable<DataPoint>>> GetDatapoints(string sensorId, DateTime fromDate, DateTime toDate);
    }
}
