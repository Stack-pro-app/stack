using Amazon.S3;
using Amazon.S3.Model;

namespace gateway_chat_server.Services
{
    public class StorageService
    {
        private readonly IAmazonS3 _client;
        public StorageService(IAmazonS3 client)
        {
            _client = client;
        }

        //File Management
        public async Task<string> UploadFile(IFormFile file,string bucketName,string prefix)
        {
            var bucketExists = await _client.DoesS3BucketExistAsync(bucketName);
            if (bucketExists) throw new Exception("Bucket Not Found");
            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = $"{prefix?.TrimEnd('/')}/{file.FileName}",
                InputStream = file.OpenReadStream(),
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _client.PutObjectAsync(request);
            return request.Key;
        }
        

        
    }
}
