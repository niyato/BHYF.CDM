using CDM.Infrastructure.Logging;
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
    public class StemExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            LogHelper.LogException(context.ExceptionContext.Exception);
            
        }
    }
}