
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVCProject.Filters
{
    public class SessionAuthorizeAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Session.GetString("Name") == null) 
            {
                context.Result = new RedirectToActionResult("Login","Login",null);
            }
            base.OnActionExecuted(context);
        }
    }
}
