using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SensorDataApp.Controllers;
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

        public SensorsController(ILogger<SensorDataController> logger, DataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<IEnumerable<Sensor>> Get()
        {
            return await _dataService.GetSensors();
        }

        [HttpPut]
        public async Task<Sensor?> Put(Sensor sensor)
        {
            return await _dataService.PutSensor(sensor);
        }
    }
}
