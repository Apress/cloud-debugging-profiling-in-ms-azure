using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("CoffeeFix.Console.Tests")]

namespace CoffeeFix.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine($"CoffeeFix Console started with {args.Length} parameters.");          

            if (args.Length != 2)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("CoffeeFix Console requires two parameters: base url and the id of the coffee maker.");
                System.Console.WriteLine("Example:");
                System.Console.WriteLine("CoffeeFix.Console https://coffeefix.com/ CB81F3C2-1182-4A5D-A941-52A80CEBE1D1");
                System.Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            var watch = new Stopwatch();
            watch.Start();

            var telemetryClient = ConfigureTelemetry(args[1]);

            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var props = new Dictionary<string, string> { { "Version", version } };
            telemetryClient.TrackEvent("CoffeeFix.Console has started!", props);

            // this simulates a coffee make sending a message back to the main website periodically
            try
            {
                WaterSensorDependency.VerifySensor();

                var task = Task.Run(() => ReportSender.SendMessageToWebSite(args[0], Guid.Parse(args[1])));
                task.Wait();
            }
            catch (Exception ex)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Failed to send telemetry: {ex.GetBaseException()}.");
            }

            telemetryClient.TrackEvent("CoffeeFix.Console has ended!",
                                        properties: props,
                                        metrics: new Dictionary<string, double>
                                        {
                                            {
                                                "ElapsedMilliseconds", watch.ElapsedMilliseconds
                                            }
                                        });

            telemetryClient.Flush();
            Task.Delay(5000).Wait();

            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("CoffeeFix Console completed.");
        }

        private static TelemetryClient ConfigureTelemetry(string coffeeMakerId)
        {
            // set the Application Insights key
            TelemetryConfiguration configuration = TelemetryConfiguration.Active;
            configuration.InstrumentationKey = "41933ea7-d572-44d2-b4d6-49df6050c592";
            TelemetryConfiguration.Active.TelemetryInitializers.Add(new TelemetryInitializer(coffeeMakerId));

            var builder = TelemetryConfiguration.Active.TelemetryProcessorChainBuilder;
            builder.Use((next) => new MyTelemetryProcessor(next));
            builder.Build();

            DependencyTrackingTelemetryModule dependencyTracking = new DependencyTrackingTelemetryModule();
            dependencyTracking.Initialize(TelemetryConfiguration.Active);

            return new TelemetryClient();            
        }
        public static bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
