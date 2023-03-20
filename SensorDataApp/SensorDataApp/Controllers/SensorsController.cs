using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SensorDataApp.Controllers;
using SensorDataApp.Hubs;
using SensorDataApp.Model;
using SensorDataApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorDataApp.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ILogger<SensorDataController> _logger;
        private DataService _dataService;
        private readonly IHubContext<SensorsHub> _sensorsHub;

        public SensorsController(ILogger<SensorDataController> logger, DataService dataService, IHubContext<SensorsHub> sensorsHub)
        {
            _logger = logger;
            _dataService = dataService;
            _sensorsHub = sensorsHub;
        }

        [HttpGet]
        public async Task<IEnumerable<Sensor>> Get()
        {
            return await _dataService.GetSensors();
        }

        [HttpPut]
        public async Task<Sensor?> Put(Sensor sensor)
        {
            try
            {
                await _dataService.GetSensor(sensor.Id);
                return await _dataService.PutSensor(sensor);
            }
            catch (Exception)
            {
                Sensor newSensor = await _dataService.AddSensor(sensor);
                if (newSensor != null && newSensor.Id != null)
                {
                    await _sensorsHub.Clients.Group("new_sensors").SendAsync("ReceiveSensor", newSensor);
                }
                return newSensor;
            }
        }
    }
}
