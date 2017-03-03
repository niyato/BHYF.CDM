using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;


namespace CDM.Infrastructure.WebApi
{
    public class StemApiController:ApiController
    {

        public StemApiController()
        {
        }
        protected ComResult Ok()
        {
            return new ComResult(this);
        }
        protected ComResult<T> Ok<T>(T content)
        {
            return Ok(content, null);
        }
        protected ComResult<T> Ok<T>(T content, Envelope envelope)
        {
            return new ComResult<T>(content, envelope, this);
        }
        protected ErrorResult BadRequest()
        {
            return new ErrorResult(HttpStatusCode.BadRequest, string.Empty, this);
        }
        protected ErrorResult BadRequest(string message)
        {
            return new ErrorResult(HttpStatusCode.BadRequest, message, this);
        }

        protected ErrorResult NotFound()
        {
            return new ErrorResult(HttpStatusCode.NotFound, string.Empty, this);
        }
    }
}
