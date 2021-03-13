using Filed_Coding.WebApi.CustomExceptions;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Filed_Coding.WebApi.DummyGatewayService
{
    public interface IExpensivePaymentGateway
    {
        bool IsAvailable { get; set; }
        Task<bool> ExecutePaymentServices();
    }

    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {
        public bool IsAvailable { get; set; } = true;

        public async Task<bool> ExecutePaymentServices()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                ///Logic for Dummy payment Gateway comes here   
                await Task.Delay(4500);
                IsAvailable = true;
                return true;
            }
            throw new PaymentGatewayException("GateWay not available");
        }
    }

}
