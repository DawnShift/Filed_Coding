using System;
using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Filed_Coding.WebApi;
using System.Threading.Tasks;
using Filed_Coding.ShearedModel.Models;
using System.Net;
using Filed_Coding.Tests.Helpers;

namespace Filed_Coding.Tests.PaymentAPITests
{
    public class PaymentTest : IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient client;
        public PaymentTest(TestFixture<Startup> fixture)
        {
            client = fixture.Client;
        }
        [Theory]
        [InlineData("POST")]
        public async Task PaymentHappyPath_Test(string method)
        {
            //Arrange  
            var paymenet = new PaymentDto
            {
                CreditCardNumber = "4111111111111111",
                CardHolder = "RahulSR",
                ExpirationDate = DateTime.Now.AddYears(1),
                Amount = 100m,
                SecurityCode = "965"
            };
            //act
            var response = await client.PostAsync("api/payment/ProcessPayment", ContentHelper.GetStringContent(paymenet));
            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Theory]
        [InlineData("POST")]
        public async Task PaymentInvalidCard_Test(string method)
        {
            //Arrange  
            var paymenet = new PaymentDto
            {
                CreditCardNumber = "4111XoXo111",
                CardHolder = "RahulSR",
                ExpirationDate = DateTime.Now.AddYears(1),
                Amount = 100m,
                SecurityCode = "965"
            };
            //act
            var response = await client.PostAsync("api/payment/ProcessPayment", ContentHelper.GetStringContent(paymenet));
            //assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("POST")]
        public async Task PaymentInvalidCardSecurityCode_Test(string method)
        {
            //Arrange  
            var paymenet = new PaymentDto
            {
                CreditCardNumber = "4111XoXo111",
                CardHolder = "RahulSR",
                ExpirationDate = DateTime.Now.AddYears(1),
                Amount = 100m,
                SecurityCode = "965222"
            };
            //act
            var response = await client.PostAsync("api/payment/ProcessPayment", ContentHelper.GetStringContent(paymenet));
            //assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("POST")]
        public async Task PaymentName_Test(string method)
        {
            //Arrange  
            var paymenet = new PaymentDto
            {
                CreditCardNumber = "4111XoXo111",
                CardHolder =null,
                ExpirationDate = DateTime.Now.AddYears(1),
                Amount = 100m,
                SecurityCode = "965"
            };
            //act
            var response = await client.PostAsync("api/payment/ProcessPayment", ContentHelper.GetStringContent(paymenet));
            //assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("POST")]
        public async Task PaymentInvalidExpiry_Test(string method)
        {
            //Arrange  
            var paymenet = new PaymentDto
            {
                CreditCardNumber = "4111XoXo111",
                CardHolder = "RahulSR",
                ExpirationDate = DateTime.Now.AddYears(-1),
                Amount = 100m,
                SecurityCode = "965"
            };
            //act
            var response = await client.PostAsync("api/payment/ProcessPayment", ContentHelper.GetStringContent(paymenet));
            //assert 
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
