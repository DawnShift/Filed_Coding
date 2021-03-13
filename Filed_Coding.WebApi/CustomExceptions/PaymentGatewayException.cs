using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Filed_Coding.WebApi.CustomExceptions
{
    [Serializable]
    public class PaymentGatewayException : Exception
    {
        public PaymentGatewayException()
        {
        }

        public PaymentGatewayException(string message) : base(message)
        {
        }

        public PaymentGatewayException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PaymentGatewayException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
