using Microsoft.AspNetCore.Mvc.Filters;

namespace ITHealth.Web.API.Infrastructure.Filters
{
    internal sealed class LocalizationAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            var acceptLanguage = headers.AcceptLanguage.FirstOrDefault();

            if (!string.IsNullOrEmpty(acceptLanguage))
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(acceptLanguage.Substring(0,5));
            }
        }
    }
}
