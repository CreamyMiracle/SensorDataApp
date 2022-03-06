using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SensorDataApp.Hubs
{
    public class SensorDataHub : Hub
    {
        public SensorDataHub(ILogger<SensorDataHub> logger)
        {
            _logger = logger;
        }
        public async Task JoinGroup([Required] string sensorId)
        {
            await Groups.AddToGroupAsync(this.Context.ConnectionId, sensorId);
        }

        #region Private Fields
        private readonly ILogger<SensorDataHub> _logger;
        #endregion
    }
}
