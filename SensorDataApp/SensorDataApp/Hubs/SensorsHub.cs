using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SensorDataApp.Hubs
{
    public class SensorsHub : Hub
    {
        public SensorsHub(ILogger<SensorsHub> logger)
        {
            _logger = logger;
        }
        public async Task JoinGroup([Required] string sensorId)
        {
            await Groups.AddToGroupAsync(this.Context.ConnectionId, sensorId);
        }

        #region Private Fields
        private readonly ILogger<SensorsHub> _logger;
        #endregion
    }
}
