using Cloudbase.Entities.TenantModels;
using CloudBase.Data.TenantProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cloudbase.Security.Filters
{
    public class TenantAttribute : ActionFilterAttribute
    {
        private Tenant _tenant { get; set; }

        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {

            //actionExecutingContext.HttpContext.RequestServices.GetService<WebTenantProvider>();

            /*            actionExecutingContext.Result = new StatusCodeResult(501);*/
            base.OnActionExecuting(actionExecutingContext);

            //var fullAddress = actionExecutingContext.HttpContext?.Request?
            //    .Headers?["Host"].ToString()?.Split('.');
            //if (fullAddress.Length < 2)
            //{
            //    actionExecutingContext.Result = new StatusCodeResult(404);
            //    base.OnActionExecuting(actionExecutingContext);
            //}
            //else
            //{
            //    var subdomain = fullAddress[0];
            //    //We got the subdomain value, next verify it from database and
            //    //inject the information to RouteContext
            //}
        }
    }
}