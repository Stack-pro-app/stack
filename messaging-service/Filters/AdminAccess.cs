using messaging_service.Repository;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using messaging_service.Repository.Interfaces;

namespace messaging_service.Filters
{
    public class AdminAccess
    {
        private readonly IWorkspaceRepository _workspacerepository;
        public AdminAccess(IWorkspaceRepository workspaceRepository)
        {
            _workspacerepository = workspaceRepository;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var AuthId = context.HttpContext.Request.Headers["AuthId"].FirstOrDefault();

            if (AuthId.IsNullOrEmpty())
            {
                context.Result = new BadRequestObjectResult("Can't Identify User");
                return;
            }

            var workspaceId = context.HttpContext.Request.RouteValues["workspaceId"]?.ToString() ??
                          context.HttpContext.Request.Form["workspaceId"].ToString();

            if (workspaceId.IsNullOrEmpty())
            {
                context.Result = new BadRequestObjectResult("can't Identify Workspace");
                return;
            }

            var result1 = await _workspacerepository.VerifyAdminStatus(AuthId?.ToString(), Convert.ToInt32(workspaceId));

            if (!result1)
            {
                context.Result = new StatusCodeResult(403);
                return;
            }

            await next();
        }
    }
}
