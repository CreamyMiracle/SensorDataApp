using Refit;
using SensorDataApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataApp
{
    public interface ISensorAPI
    {
        [Get("/")]
        Task<ApiResponse<IEnumerable<Sensor>>> GetSensors();
    }
}