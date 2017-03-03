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
    public class ComResult : IHttpActionResult
    {
        private ApiController _controller;

        public ComResult(ApiController controller)
        {
            _controller = controller;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _controller.Request.CreateResponse(HttpStatusCode.NoContent);
            return Task.FromResult(response);
        }
    }

    public class ComResult<T> : IHttpActionResult
    {
        private T _content;
        private Envelope _envelope;
        private ApiController _controller;

        public ComResult(T content, Envelope envelope, ApiController controller)
        {
            _content = content;
            _envelope = envelope;
            _controller = controller;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var payload = new Envelope<T> { data = _content };
            if (null != _envelope)
            {
                payload.limit = _envelope.Limit;
                payload.page = _envelope.Page;
                payload.total = _envelope.Total;
            }
            var response = _controller.Request.CreateResponse(HttpStatusCode.OK, payload);
            return Task.FromResult(response);
        }
    }
}
