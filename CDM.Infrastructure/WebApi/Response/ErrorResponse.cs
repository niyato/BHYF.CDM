using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CDM.Infrastructure.WebApi
{
    public class ErrorResponse
    {
        public string code { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public Exception exception { get; set; }
        public List<ModelError> errors { get; set; }
    }

    public class ModelError
    {
        public string field { get; set; }
        public string[] messages { get; set; }
    }
}
