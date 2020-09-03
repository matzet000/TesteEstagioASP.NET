using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Api.Filters
{
    public class ErrorFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ArgumentException)
            {
                var erroMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                erroMessage.Content = new StringContent(context.Exception.Message);
                context.Response = erroMessage;
            }

            base.OnException(context);
        }
    }
}