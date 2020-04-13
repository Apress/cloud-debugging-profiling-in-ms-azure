using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeFix.Console
{
    class WaterSensorDependency
    {
        public static void VerifySensor()
        {
            var someRandomness = new Random();
            try
            {
                if (someRandomness.Next(1, 100) > 97)
                    throw new Exception("Failure verifying if WaterSensor is online.");

                var isFailureResponse = someRandomness.Next(1, 100) > 85;

                var telemetryClient = new TelemetryClient();
                telemetryClient.TrackDependency(dependencyTypeName: "Application",
                                                dependencyName: "WaterSensor",
                                                target: "IsSystemOnline?",
                                                data: "{ 'data' : 'somedata' }",
                                                startTime: DateTime.UtcNow,
                                                duration:  new TimeSpan(someRandomness.Next(200, 3300)),
                                                resultCode: isFailureResponse ? $"Sensor {someRandomness.Next(21, 3232)}" : "",
                                                success: isFailureResponse ? false : true);
            }
            catch (Exception ex)
            {
                var telemetryClient = new TelemetryClient();
                telemetryClient.TrackException(ex);
            }
        }
    }
}
