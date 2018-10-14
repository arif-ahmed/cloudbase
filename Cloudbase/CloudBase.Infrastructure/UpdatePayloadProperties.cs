using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudBase.Infrastructure
{
    public class UpdatePayloadProperties : ActionFilterAttribute
    {
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var stream = request.Body;

            var reader = new StreamReader(stream);
            reader.BaseStream.Position = 0;
            var originalContent = reader.ReadToEnd();
            var dataSource = JsonConvert.DeserializeObject(originalContent);
            var json = JsonConvert.SerializeObject(dataSource);

            var test = JObject.Parse(json);
            test.Add("CreatedBy", "YoYoKids");

            var str = test.ToString();

            var requestContent = new StringContent(str, Encoding.UTF8, "application/json");
            stream = await requestContent.ReadAsStreamAsync();
            context.HttpContext.Request.Body = stream;
            
            base.OnActionExecuting(context);
        }

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    var obj = context.HttpContext.Request.Body;
        //    base.OnActionExecuted(context);
        //}
    }
}