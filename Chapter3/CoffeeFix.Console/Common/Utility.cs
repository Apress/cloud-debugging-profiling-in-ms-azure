using CoffeeFix.Console.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CoffeeFix.Console.Common
{
    public class Utility
    {
        public static string name;
        public static Guid makerId;
        public static string localPathofReportFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string localReportFileName = "Report_" + Guid.NewGuid().ToString() + ".txt";
        public static void PrepareReport()
        {
            var reportData = GenerateRandomDataForReport(name, makerId);
            File.WriteAllBytes(SourceFile, reportData);
        }

        public static byte[] GenerateRandomDataForReport(string name, Guid makerId)
        {
            var random = new Random();

            var report = new StoreReport
            {
                MakerId = makerId,
                Supervisor = name,
                UpTimeMeasure = random.Next(300, 22000),
                BeanCount = random.Next(100, 33000),
                WaterLevel = random.Next(0, 9),
                RandomMetricsA = Enumerable.Repeat(0, 500).Select(i => random.Next(0, 99)).ToArray(),
                RandomMetricsB = Enumerable.Repeat(0, 500).Select(i => random.Next(0, 99)).ToArray(),
                RandomMetricsC = Enumerable.Repeat(0, 500).Select(i => random.Next(0, 99)).ToArray(),
            };

            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(report, Formatting.None));
        }
        public static string SourceFile => Path.Combine(localPathofReportFile, localReportFileName);
    }
}
