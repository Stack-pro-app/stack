using messaging_service.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace messaging_service.Filters
{
    public class ChannelAccess
    {
        private readonly IChannelRepository _repository;
        public ChannelAccess(IChannelRepository channelRepository)
        {
            _repository = channelRepository;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var AuthId = context.HttpContext.Request.Headers["AuthId"].FirstOrDefault();

            if (AuthId.IsNullOrEmpty())
            {
                context.Result = new BadRequestObjectResult("Can't Identify User");
                return;
            }

            var channelId = context.HttpContext.Request.RouteValues["channelId"]?.ToString() ??
                          context.HttpContext.Request.Form["channelId"].ToString();

            if (channelId.IsNullOrEmpty())
            {
                context.Result = new BadRequestObjectResult("can't Identify Workspace");
                return;
            }

            var result1 = await _repository.VerifyAccess(AuthId?.ToString(), Convert.ToInt32(channelId));

            if (!result1)
            {
                context.Result = new StatusCodeResult(403);
                return;
            }

            await next();
        }
    }
}
