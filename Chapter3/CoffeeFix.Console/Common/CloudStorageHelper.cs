using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace CoffeeFix.Console.Common
{
    public class CloudStorageHelper
    {
        public static async Task PrepareReportAndUploadToAzureBlob(string name, Guid makerId)
        {
            string coffeeFixConnectionString = Environment.GetEnvironmentVariable("COFFEEFIXCONNECT_STR");
            CloudStorageAccount coffeeFixStorageAccount;
            if (CloudStorageAccount.TryParse(coffeeFixConnectionString, out coffeeFixStorageAccount))
            {
                CloudBlobClient cloudBlobClient = coffeeFixStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("container-reports");
                await cloudBlobContainer.CreateIfNotExistsAsync();

                //setting permissions are optional
                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await cloudBlobContainer.SetPermissionsAsync(permissions);

                Utility.name = name;
                Utility.makerId = makerId;
                Utility.PrepareReport();
                System.Console.WriteLine($"File is created {Utility.SourceFile}");
                System.Console.WriteLine("Uploading to Blob storage as blob '{0}'", Utility.localReportFileName);

                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(Utility.localReportFileName);
                await cloudBlockBlob.UploadFromFileAsync(Utility.SourceFile);
            }
            else
            {
                throw new Exception("A connection string has not been defined in the system environment variables. " +
         "Add an environment variable named 'COFFEEFIXCONNECT_STR' with your storage " +
         "connection string as a value.");

            }

        }
    }
}