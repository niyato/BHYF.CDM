using CDM.Infrastructure.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace CDM.Infrastructure.WebApi.ExceptionHandling
{
    public class StemExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new ErrorResult(HttpStatusCode.InternalServerError,
                "Sorry! Something went wrong. Please contact support@youjiaoyun.net so we can try to fix it.",
                context.ExceptionContext.Request, context.ExceptionContext.Exception);
        }
    }
}