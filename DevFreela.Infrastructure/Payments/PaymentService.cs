using System.Text;
using System.Text.Json;
using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBusService;

        public PaymentService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        private const string QUEUE_NAME = "Payments";

        public void ProcessPayment(PaymentInfoDto paymentInfoDto)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDto);
            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);
            _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }
}
