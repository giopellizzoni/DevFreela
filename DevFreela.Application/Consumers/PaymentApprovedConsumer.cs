﻿using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System.Threading.Channels;
using DevFreela.Core.IntegrationEvents;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.UnityOfWork;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Events;

namespace DevFreela.Application.Consumers
{
    public  class PaymentApprovedConsumer : BackgroundService
    {
        private const string PAYMENT_APPROVED_QUEUE = "PaymentsApproved";

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public PaymentApprovedConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: PAYMENT_APPROVED_QUEUE, durable: false, exclusive: false, autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, eventArgs) =>
            {
                var paymentApprovedBytes = eventArgs.Body.ToArray();
                var paymentApprovedJson = Encoding.UTF8.GetString(paymentApprovedBytes);

                var paymentApprovedIntegrationEvent =
                    JsonSerializer.Deserialize<PaymentApprovedIntegrationEvent>(paymentApprovedJson);
                await FinishProject(paymentApprovedIntegrationEvent.IdProject);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(PAYMENT_APPROVED_QUEUE, false, consumer);
            return Task.CompletedTask;
        }

        public async Task FinishProject(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var unityOfWork = scope.ServiceProvider.GetRequiredService<IUnityOfWork>();

                var project = await unityOfWork.Projects.GetByIdAsync(id);
                await unityOfWork.BeginTransactionAsync();
                project?.Finish();
                await unityOfWork.SaveChangesAsync();

                await unityOfWork.CommitTransactionAsync();
            }
        }
    }
}
