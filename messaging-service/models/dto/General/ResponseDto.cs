using Microsoft.AspNetCore.Mvc;

namespace messaging_service.models.dto.Response
{
    public class ResponseDto
    {
        public Object? Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }
}
