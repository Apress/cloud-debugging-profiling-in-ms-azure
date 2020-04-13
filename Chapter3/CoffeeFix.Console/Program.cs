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
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace CoffeeFix.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                if (args.Length == 2)
                {
                    var task = Task.Run(() => Common.CloudStorageHelper.PrepareReportAndUploadToAzureBlob(args[0], Guid.Parse(args[1])));
                    task.Wait();
                }
                else
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("CoffeeFix Console requires two parameters: Store Supervisorname and the id of the coffee maker.");
                    System.Console.WriteLine("Example:");
                    System.Console.WriteLine("CoffeeFix.Console Kenneth Davis CB81F3C2-1182-4A5D-A941-52A80CEBE1D1");
                }
            }
            catch (Exception ex)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Failed to send report: {ex.GetBaseException()}.");
            }

            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("CoffeeFix Console completed.");
        }

    }
}