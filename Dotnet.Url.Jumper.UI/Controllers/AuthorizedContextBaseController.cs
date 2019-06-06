using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Dotnet.Url.Jumper.UI.Controllers
{
    public class AuthorizedContextBaseController : Controller
    {
        protected ClaimsPrincipal _currentuser;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _currentuser = context.HttpContext.User;
            base.OnActionExecuting(context);
        }
    }
}
