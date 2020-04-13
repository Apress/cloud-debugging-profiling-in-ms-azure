using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CoffeeFix.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // this simulates a coffee make sending a message back to the main website periodically
            try
            {
                if (args.Length == 2)
                {
                    var task = Task.Run(() => SendMessageToWebSite(args[0], Guid.Parse(args[1])));
                    task.Wait();
                }
                else
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("CoffeeFix Console requires two parameters: base url and the id of the coffee maker.");
                    System.Console.WriteLine("Example:");
                    System.Console.WriteLine("CoffeeFix.Console https://coffeefix.com/api/telemetry CB81F3C2-1182-4A5D-A941-52A80CEBE1D1");
                }
            }
            catch (Exception ex)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Failed to send telemetry: {ex.GetBaseException()}.");
            }

            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("CoffeeFix Console completed.");
        }

        static async Task SendMessageToWebSite(string baseurl, Guid makerId)
        {
            using (var stream = new MemoryStream(RandomTelemetry(makerId)))
            {
                using (var fileContent = new StreamContent(stream))
                {
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"file\"",
                        FileName = "\"telemetry.txt\""
                    };
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(baseurl);

                        MultipartFormDataContent multiContent = new MultipartFormDataContent();
                        multiContent.Add(new StringContent(makerId.ToString()), "coffeemakerid");
                        multiContent.Add(new StringContent(DateTime.Now.ToString("o")), "date");
                        multiContent.Add(new StringContent("WarningMaintenanceDue"), $"status");
                        multiContent.Add(fileContent);

                        var result = await client.PostAsync($"api/telemetry", multiContent);

                        if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            System.Console.ForegroundColor = ConsoleColor.Green;
                            System.Console.WriteLine("CoffeeFix Console successfully submitted telemetry.");
                        }
                        else
                        {
                            System.Console.ForegroundColor = ConsoleColor.DarkYellow;
                            System.Console.WriteLine($"CoffeeFix Console received a response of {result.StatusCode}.");
                        }
                    }
                }
            }
        }

        public static byte[] RandomTelemetry(Guid makerId)
        {
            var random = new Random();

            var report = new TelemetryReport
            {
                MakerId = makerId,
                UpTimeMeasure = random.Next(300, 22000),
                BeanCount = random.Next(100, 33000),
                WaterLevel = random.Next(0, 9),
                RandomMetricsA = Enumerable.Repeat(0, 500).Select(i => random.Next(0, 99)).ToArray(),
                RandomMetricsB = Enumerable.Repeat(0, 500).Select(i => random.Next(0, 99)).ToArray(),
                RandomMetricsC = Enumerable.Repeat(0, 500).Select(i => random.Next(0, 99)).ToArray(),
            };

            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(report, Newtonsoft.Json.Formatting.None));
        }

        public class TelemetryReport
        {
            public Guid MakerId { get; set; }
            public int UpTimeMeasure { get; set; }
            public int BeanCount { get; set; }
            public int WaterLevel { get; set; }
            public int[] RandomMetricsA { get; set; }
            public int[] RandomMetricsB { get; set; }
            public int[] RandomMetricsC { get; set; }
        }


    }
}
