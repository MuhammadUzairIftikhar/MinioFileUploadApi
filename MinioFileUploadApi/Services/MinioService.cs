using Minio;
using Minio.Exceptions;
using Minio.DataModel.Args;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MinioFileUploadApi.Services
{
    public class MinioService
    {
        private readonly MinioClient _minioClient;
        private readonly string _bucketName;

        public MinioService(MinioClient minioClient)
        {
            _minioClient = minioClient;
        }
        public MinioService(MinioClient minioClient, IConfiguration configuration)
        {
            _minioClient = minioClient;
            _bucketName = configuration["Minio:BucketName"];
        }

        public async Task UploadFileAsync(string objectName, Stream fileStream, string contentType)
        {
            try
            {
                var args = new PutObjectArgs()
                    .WithBucket(_bucketName)
                    .WithObject(objectName)
                    .WithStreamData(fileStream)
                    .WithObjectSize(fileStream.Length)
                    .WithContentType(contentType);

                await _minioClient.PutObjectAsync(args);
            }
            catch (MinioException e)
            {
                throw new Exception($"File upload failed: {e.Message}");
            }
        }
    }
}
