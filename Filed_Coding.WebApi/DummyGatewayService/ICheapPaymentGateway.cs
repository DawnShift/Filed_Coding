using Filed_Coding.WebApi.CustomExceptions;
using System.Threading.Tasks;

namespace Filed_Coding.WebApi.DummyGatewayService
{
    public interface ICheapPaymentGateway
    {
        bool IsAvailable { get; set; }
        Task<bool> ExecutePaymentServices();
    }

    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        public bool IsAvailable { get; set; } = true;

        public Task<bool> ExecutePaymentServices()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                ///Logic for Dummy payment Gateway comes here   
                Task.Delay(4500);
                IsAvailable = true;
                return Task.FromResult(true);
            }
            throw new PaymentGatewayException("GateWay not available");
        }
    }
}
