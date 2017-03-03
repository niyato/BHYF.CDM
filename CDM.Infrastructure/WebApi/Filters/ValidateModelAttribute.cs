using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CDM.Infrastructure.WebApi.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if(!actionContext.ModelState.IsValid)
            {
                var payload = new ErrorResponse() { message = "请求无效。", errors = new List<ModelError>() };
                foreach (var pair in actionContext.ModelState)
                {
                    if (0 != pair.Value.Errors.Count)
                    {
                        payload.errors.Add(new ModelError()
                        {
                            field = pair.Key,
                            messages = pair.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        });
                    }
                }
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, payload);
            }
        }
    }
}