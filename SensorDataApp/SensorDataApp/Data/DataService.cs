using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SensorDataApp.Model;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace SensorDataApp.Service
{
    public class DataService
    {
        private readonly SQLiteAsyncConnection db_con;

        public DataService(SQLiteAsyncConnection con)
        {
            db_con = con;
        }

        public async Task<IEnumerable<Sensor>> GetSensors()
        {
            return await db_con.GetAllWithChildrenAsync<Sensor>();
        }

        public async Task<Sensor> GetSensor(string sensorId)
        {
            return await db_con.GetAsync<Sensor>(sensorId);
        }

        public async Task<Sensor?> AddSensor(Sensor sensor)
        {
            return await db_con.InsertAsync(sensor) == 1 ? sensor : null;
        }

        public async Task<Sensor?> PutSensor(Sensor sensor)
        {
            return await db_con.UpdateAsync(sensor) == 1 ? sensor : null;
        }

        public async Task<IEnumerable<DataPoint>> GetSensorDataPoints(string sensorId, DateTime fromDate, DateTime toDate)
        {
            return await db_con.GetAllWithChildrenAsync<DataPoint>(dp => dp.SensorId == sensorId && dp.Timestamp > fromDate && dp.Timestamp <= toDate);
        }

        public async Task<DataPoint?> InsertDataPoint(DataPoint datapoint)
        {
            return await db_con.InsertAsync(datapoint) == 1 ? datapoint : null;
        }

        #region Private Methods

        #endregion
    }
}
