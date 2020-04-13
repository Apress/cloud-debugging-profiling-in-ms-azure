using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoffeeFix.Web.Api
{
    [Route("api/[controller]")]
    public class TelemetryController : Controller
    {
        private Data.ApplicationDbContext _context;

        public TelemetryController(Data.ApplicationDbContext context)
        {
            _context = context;
        }


        // POST api/<controller>
        [HttpPost]
        public async Task Post([FromForm]Models.CoffeeMakerTelemetry telemetry, IFormFile file)
        {
            var folder = $"telemetry/{telemetry.CoffeeMakerId}";

            Directory.CreateDirectory(folder);

            telemetry.DataFileName = $"{folder}/{telemetry.Date.ToString("yyyyMMdd-hhmmss")}.txt";

            using (var stream = new FileStream(telemetry.DataFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
                        
            _context.Add(telemetry);
            await _context.SaveChangesAsync();
        }
    }
}
