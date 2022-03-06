using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SensorDataApp.Service;
using SensorDataApp.Hubs;
using SensorDataApp.Model;

namespace SensorDataApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorDataController : ControllerBase
    {
        private readonly ILogger<SensorDataController> _logger;
        private readonly IHubContext<SensorDataHub> _sensorDataHub;
        private DataService _dataService;

        public SensorDataController(ILogger<SensorDataController> logger, DataService dataService, IHubContext<SensorDataHub> sensorDataHub)
        {
            _logger = logger;
            _dataService = dataService;
            _sensorDataHub = sensorDataHub;
        }

        [HttpGet("{sensorId}")]
        public async Task<IEnumerable<DataPoint>> Get(string sensorId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            return await _dataService.GetSensorDataPoints(sensorId, fromDate, toDate);
        }

        [HttpPost]
        public async Task<DataPoint?> Post(DataPoint datapoint)
        {
            try
            {
                await _dataService.GetSensor(datapoint.SensorId);
            }
            catch (Exception ex)
            {
                Sensor newSensor = new Sensor() { Id = datapoint.SensorId };
                await _dataService.AddSensor(new Sensor() { Id = datapoint.SensorId });
            }

            DataPoint dp = await _dataService.InsertDataPoint(datapoint);
            if (dp != null)
            {
                await _sensorDataHub.Clients.Group(dp.SensorId).SendAsync("ReceiveDataPoint", dp);
            }
            return dp;
        }
    }
}
