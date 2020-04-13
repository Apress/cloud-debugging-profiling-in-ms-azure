using System;

namespace CoffeeFix.Console.Models
{
    public class StoreReport
    {
        public Guid MakerId { get; set; }
        public string Supervisor { get; set; }
        public int UpTimeMeasure { get; set; }
        public int BeanCount { get; set; }
        public int WaterLevel { get; set; }
        public int[] RandomMetricsA { get; set; }
        public int[] RandomMetricsB { get; set; }
        public int[] RandomMetricsC { get; set; }
    }
}
