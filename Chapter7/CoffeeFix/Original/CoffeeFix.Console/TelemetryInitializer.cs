using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace CoffeeFix.Console
{
    internal class TelemetryInitializer : ITelemetryInitializer
    {
        private string _makerId;
        private string _sessionId;

        public TelemetryInitializer(string userId)
        {
            _makerId = userId;
            _sessionId = Guid.NewGuid().ToString();
        }

        public void Initialize(ITelemetry telemetry)
        {
            var context = telemetry.Context;

            context.Session.Id = _sessionId;
            context.User.Id = _makerId;
        }
    }
}
