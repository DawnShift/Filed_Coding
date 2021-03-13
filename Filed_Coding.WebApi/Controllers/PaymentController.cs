using AutoMapper;
using Filed_Coding.Data.Models;
using Filed_Coding.Data.Services;
using Filed_Coding.Helpers.Extinsion;
using Filed_Coding.ShearedModel.Models;
using Filed_Coding.WebApi.CustomExceptions;
using Filed_Coding.WebApi.DummyGatewayService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Filed_Coding.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRepository<Payment, Guid> paymentRepo;
        private readonly ICheapPaymentGateway cheapPaymentService;
        private readonly IExpensivePaymentGateway expensivePaymentGateway;

        public PaymentController(IMapper _mapper, IRepository<Payment, Guid> __paymentRepo,
                                ICheapPaymentGateway _cheapPaymentService,
                                 IExpensivePaymentGateway _expensivePaymentGateway)
        {
            mapper = _mapper;
            paymentRepo = __paymentRepo;
            cheapPaymentService = _cheapPaymentService;
            expensivePaymentGateway = _expensivePaymentGateway;
        }
        [HttpPost]
        [Route("ProcessPayment")]
        public IActionResult Post([FromBody] PaymentDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var payment = mapper.Map<Payment>(model);
                    ///Note : the question does not account for value 20
                    bool result = false;
                    if (model.Amount <= 20)
                        result = Retry.Do<bool>(ExecuteCheapPayment, TimeSpan.FromSeconds(30), 1);
                    else if (model.Amount > 20 && model.Amount <= 500)
                    {
                        if (expensivePaymentGateway.IsAvailable)
                            result = Retry.Do<bool>(ExecuteExpensivePayment, TimeSpan.FromSeconds(30), 1);
                        else
                            result = Retry.Do<bool>(ExecuteCheapPayment, TimeSpan.FromSeconds(30), 1);
                    }
                    else
                        result = Retry.Do<bool>(ExecuteExpensivePayment, TimeSpan.FromSeconds(30), 3);
                    if (result)
                    {
                        if (payment.Id == Guid.Empty)
                            paymentRepo.Insert(payment);
                        else
                            paymentRepo.Update(payment);
                        return Ok(mapper.Map<PaymentDto>(payment));
                    }
                    throw new HttpInternelServerErrorException(HttpStatusCode.InternalServerError, $"Something went wrong.Additional Information: Payment Gateway Failure");
                }
                catch (Exception ex)
                {
                    throw new HttpInternelServerErrorException(HttpStatusCode.InternalServerError, $"Something went wrong.Additional Information:{ex.Message}");
                }
            }
            return BadRequest(ModelState);
        }


        public bool ExecuteCheapPayment()
        {
            return cheapPaymentService.ExecutePaymentServices().GetAwaiter().GetResult();
        }
        public bool ExecuteExpensivePayment()
        {
            return expensivePaymentGateway.ExecutePaymentServices().GetAwaiter().GetResult();
        }
    }
}
