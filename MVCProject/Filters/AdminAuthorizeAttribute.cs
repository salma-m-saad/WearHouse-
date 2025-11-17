
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVCProject.Filters
{
    public class AdminAuthorizeAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Session.GetString("IsAdmin") == "NotAdmin") 
            {
                context.Result = new RedirectToActionResult("Index","Homepage",null);
            }
            base.OnActionExecuted(context);
        }
    }
}
