using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CDM.Infrastructure.WebApi
{
    public class ErrorResult : IHttpActionResult
    {
        private HttpStatusCode _httpStatusCode;
        private string _message;
        private Exception _exception;
        private HttpRequestMessage Request;

        public ErrorResult(HttpStatusCode httpStatusCode, string message, ApiController controller, Exception exception = null)
        {
            _httpStatusCode = httpStatusCode;
            _message = message;
            _exception = exception;
            Request = controller.Request;
        }

        public ErrorResult(HttpStatusCode httpStatusCode, string message, HttpRequestMessage request, Exception exception = null)
        {
            _httpStatusCode = httpStatusCode;
            _message = message;
            _exception = exception;
            Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var payload = new ErrorResponse();
            if (null != _exception)
            {
                payload.exception = _exception;
                payload.type = _exception.GetType().ToString();
            }
            payload.message = null == _message ? string.Empty : _message;
            var response = Request.CreateResponse(_httpStatusCode, payload);
            return Task.FromResult(response);
        }
    }
}
