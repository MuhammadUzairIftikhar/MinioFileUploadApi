using Minio;
using Minio.DataModel.Args;
using System;
using System.Threading.Tasks;

namespace MinioTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // MinIO client configuration
            string endpoint = "http://10.0.0.186:9000"; // Your MinIO endpoint
            string accessKey = "mynewadmin"; // Your MinIO access key
            string secretKey = "mystrongpassword123"; // Your MinIO secret key
            string bucketName = "mynewbucket"; // The bucket name to upload to
            string objectName = "test.txt"; // The name of the object in MinIO
            string filePath = "C:\\Users\\sheik\\source\\repos\\MinioTest\\MinioTest\\bin\\Debug\\test.txt"; // The local file path

            var client = new MinioClient()
               .WithEndpoint("10.0.0.186:9000")
               .WithCredentials(accessKey, secretKey)
               .Build();

            var args1 = new PutObjectArgs()
              .WithBucket(bucketName)
              .WithObject(objectName)
              .WithContentType("application/octet-stream")
              .WithFileName(filePath);

            try
            {
                await client.PutObjectAsync(args1).ConfigureAwait(false);
                Console.WriteLine("File uploaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
