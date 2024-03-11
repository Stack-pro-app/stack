using Amazon.S3;

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

        

        
    }
}
