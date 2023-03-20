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
        private readonly IHubContext<SensorsHub> _sensorsHub;
        private DataService _dataService;

        public SensorDataController(ILogger<SensorDataController> logger, DataService dataService, IHubContext<SensorDataHub> sensorDataHub, IHubContext<SensorsHub> sensorsHub)
        {
            _logger = logger;
            _dataService = dataService;
            _sensorDataHub = sensorDataHub;
            _sensorsHub = sensorsHub;
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
                await _dataService.GetSensor(datapoint?.SensorId);
            }
            catch (Exception)
            {
                Sensor newSensor = new Sensor() { Id = datapoint.SensorId };
                await _dataService.AddSensor(new Sensor() { Id = datapoint.SensorId });
                if (newSensor != null && newSensor.Id != null)
                {
                    await _sensorsHub.Clients.Group("new_sensors").SendAsync("ReceiveSensor", newSensor);
                }
            }

            DataPoint? dp = await _dataService.InsertDataPoint(datapoint);
            if (dp != null && dp.SensorId != null)
            {
                await _sensorDataHub.Clients.Group(dp.SensorId).SendAsync("ReceiveDataPoint", dp);
            }
            return dp;
        }
    }
}
