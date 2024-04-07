using System.Text;
using System.Text.Json;
using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Payments
{
    public class PaymentService(IMessageBusService messageBusService)
        : IPaymentService
    {
        private const string QUEUE_NAME = "Payments";

        public void ProcessPayment(PaymentInfoDto paymentInfoDto)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDto);
            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);
            messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }
}
