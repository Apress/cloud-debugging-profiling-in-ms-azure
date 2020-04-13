using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeFix.Web.Models
{
    public class CoffeeMakerTelemetry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public Guid CoffeeMakerId { get; set; }
        public DateTime Date { get; set; }
        public CoffeeMakerStatus Status { get; set; }
        public string DataFileName { get; set; }
    }

    public enum CoffeeMakerStatus
    {
        Online,        
        ErrorWaterFailure,
        ErrorBeanFailure,
        WarningMaintenanceDue
    }

}
