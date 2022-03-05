using Refit;
using SensorDataApp.Model;

namespace SensorDataApp.Data
{
    public class SensorService
    {
        private static string _server = "http://localhost:5181";
        private readonly ISensorAPI _sensorApiClient;
        private readonly ISensorDataPointAPI _sensorDataPointApiClient;

        public SensorService()
        {
            _sensorApiClient = RestService.For<ISensorAPI>(new HttpClient()
            {
                BaseAddress = new Uri(_server + "/Sensors"),
            });

            _sensorDataPointApiClient = RestService.For<ISensorDataPointAPI>(new HttpClient()
            {
                BaseAddress = new Uri(_server + "/SensorData"),
            });
        }

        public async Task<IEnumerable<Sensor>?> GetSensors()
        {
            return (await _sensorApiClient.GetSensors()).Content;
        }

        public async Task<IEnumerable<DataPoint>?> GetSensorDataPoints(string sensorId, DateTime fromDate, DateTime toDate)
        {
            return (await _sensorDataPointApiClient.GetDatapoints(sensorId, fromDate, toDate)).Content;
        }
    }
}