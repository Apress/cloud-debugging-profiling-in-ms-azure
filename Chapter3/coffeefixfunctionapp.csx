#r "Newtonsoft.Json"

using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("Coffee report is being generated...");

    string name = req.Query["name"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    name = name ?? data?.name;
    string desc = name != null ? $"Fantastic Coffee made by {name}" : "Who made this coffee?";
    Guid reportId = Guid.NewGuid();
	var report = new {
        ReportId = reportId,
        Maker = name !=null ? name:"no name",
        Desc = desc,
        Pass = name != null
    };
    
	byte[] filebytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(report, Newtonsoft.Json.Formatting.None));
    log.LogInformation("Coffee report is generated.");
	
    return new FileContentResult(filebytes, "application/octet-stream") {FileDownloadName = $"{reportId}.txt"};

}