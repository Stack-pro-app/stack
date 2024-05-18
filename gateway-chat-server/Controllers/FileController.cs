
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using gateway_chat_server.Hubs;
using gateway_chat_server.Models;
using Amazon.S3;
using Amazon.S3.Model;
using gateway_chat_server.Producer;
using Newtonsoft.Json;

namespace gateway_chat_server.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly IHubContext<ChannelHub> _hubContext;
        private readonly IAmazonS3 _s3Client;
        private readonly IMessageProducer _producer;
        private const long MaxFileSize = 50 * 1024 * 1024;

        public FileController(IHubContext<ChannelHub> hubContext,IAmazonS3 s3Client,IMessageProducer producer)
        {
                _hubContext = hubContext;
                _s3Client = s3Client;
                _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] FileDto file)
        {
            if (file == null || file.FormFile?.Length == 0)
            {
                return BadRequest("No file uploaded or file is empty.");
            }

            if (file.FormFile.Length > MaxFileSize)
            {
                return BadRequest($"File size exceeds the limit of {MaxFileSize / (1024 * 1024)} MB.");
            }

            var bucketName = "stack-messaging-service";
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
            if (!bucketExists) return NotFound($"Bucket not found");
            var filePath = $"{file.ChannelString?.TrimEnd('/')}/{file.FormFile.FileName}";
            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = string.IsNullOrEmpty(file.ChannelString) ? file.FormFile.FileName : filePath,
                InputStream = file.FormFile.OpenReadStream(),
            };
            request.Metadata.Add("Content-Type",file.FormFile.ContentType);
            await _s3Client.PutObjectAsync(request);
            var urlRequest = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = filePath,
                Expires = DateTime.UtcNow.AddYears(1) // Set the expiration time to a far-future date
            };
            string url = _s3Client.GetPreSignedURL(urlRequest);

            ChatDto fileRequest = new()
            {
                ChannelId = file.ChannelId,
                UserId = file.UserId,
                Attachement_Name = file.FormFile.FileName,
                Attachement_Url = url,
                Attachement_Key = filePath,
                Message = file.Message,
                MessageId = Guid.NewGuid()
            };

            _producer.SendMessage(fileRequest);

            string jsonData = JsonConvert.SerializeObject(fileRequest);
            await _hubContext.Clients.Group(file.ChannelString??"").SendAsync("messageReceived", jsonData);


            return Ok(fileRequest);
        }

    }
}
