using Amazon.S3;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace awssdktest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting!");

            try
            {
                ListBucketsAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("Done!");
        }

        private static async Task ListBucketsAsync()
        {
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var options = configBuilder.GetAWSOptions();
            IAmazonS3 client = options.CreateServiceClient<IAmazonS3>();

            var bucketResponse = await client.ListBucketsAsync();
            var buckets = bucketResponse.Buckets.Select(b => b.BucketName).ToList();
            Console.WriteLine("BUCKETS:");
            foreach (var bucket in buckets)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  {bucket}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
