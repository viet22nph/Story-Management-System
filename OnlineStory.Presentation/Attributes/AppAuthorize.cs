

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using OnlineStory.Application.Abstractions.Security;

namespace OnlineStory.Presentation.Attributes;


public class AppAuthorizeAttribute : TypeFilterAttribute
{
    public AppAuthorizeAttribute(string resource, string action) : base(typeof(AppAuthorizeFilter))
    {
        Arguments = new object[] { resource, action };
    }
}

public class AppAuthorizeFilter : IAuthorizationFilter
{
    private string _resource;
    private string _action;
    private readonly ISecurityService _securityService;

    public AppAuthorizeFilter(string resource, string action, ISecurityService securityService)
    {
        _resource = resource;
        _action = action;
        _securityService = securityService;
    }

    public  void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user is  null || !user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        var userId = user.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;

        if (userId is not null)
        {
            // check permission xem nó có hợp lệ không
            var checkAccess =  _securityService.UserHasPermissionAsync(userId, _resource, _action).Result;
            if (!checkAccess)
            {
                context.Result = new ForbidResult();
                return;
            }

        }
        else
        {
            context.Result = new UnauthorizedResult();
        }


    }


}