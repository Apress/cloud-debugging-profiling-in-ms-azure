using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace CoffeeFix.Console
{
    internal class MyTelemetryProcessor : ITelemetryProcessor
    {
        private ITelemetryProcessor Next { get; set; }

        public MyTelemetryProcessor(ITelemetryProcessor next)
        {
            this.Next = next;
        }

        public void Process(ITelemetry item)
        {
            var eventTelemetry = item as EventTelemetry;

            if (eventTelemetry != null &&
                eventTelemetry.Name == "CoffeeFix.Console has started!")
            {
                // skip this item
                return;
            }

            this.Next.Process(item);
        }
    }

}
