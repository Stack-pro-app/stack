using messaging_service.Repository;
using messaging_service.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace messaging_service.Filters
{
    public class WorkspaceAccessFilter:IAsyncActionFilter
    {
        private readonly IWorkspaceRepository _workspacerepository;
        public WorkspaceAccessFilter(IWorkspaceRepository workspaceRepository)
        {
            _workspacerepository = workspaceRepository;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var AuthId = context.HttpContext.Request.Headers["AuthId"].FirstOrDefault();

            if(AuthId.IsNullOrEmpty())
            {
                context.Result = new BadRequestObjectResult("Can't Identify User");
                return;
            }

            var workspaceId = context.HttpContext.Request.RouteValues["workspaceId"]?.ToString() ??
                          context.HttpContext.Request.Form["workspaceId"].ToString();

            if(workspaceId.IsNullOrEmpty())
            {
                context.Result = new BadRequestObjectResult("can't Identify Workspace");
                return;
            }

            var result1 = await _workspacerepository.VerifyMembershipWorkspace(AuthId?.ToString(),Convert.ToInt32(workspaceId));

            if (!result1)
            {
                context.Result = new StatusCodeResult(403);
                return;
            }

            await next();
        }
    }
}
