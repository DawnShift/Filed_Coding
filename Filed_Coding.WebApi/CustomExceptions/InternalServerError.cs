using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Filed_Coding.WebApi.CustomExceptions
{
    [Serializable]
    public class HttpInternelServerErrorException : Exception
    {
        public HttpStatusCode Status { get; private set; }
        public HttpInternelServerErrorException()
        {
        }

        public HttpInternelServerErrorException(string message) : base(message)
        {
        }
        public HttpInternelServerErrorException(HttpStatusCode code, string message) : base(message)
        {
            Status = code;
        }

        public HttpInternelServerErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpInternelServerErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
