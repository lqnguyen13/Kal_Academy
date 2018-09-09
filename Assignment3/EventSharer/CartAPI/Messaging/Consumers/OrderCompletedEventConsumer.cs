using System;
using MassTransit;
using Microsoft.Extensions.Logging;
using CartAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Messaging;

namespace CartAPI.Messaging.Consumers
{
    public class OrderCompletedEventConsumer : IConsumer<OrderCompletedEvent>
    {
        private readonly ICartRepository _repository;
        private readonly ILogger<OrderCompletedEventConsumer> _logger;
        public OrderCompletedEventConsumer(ICartRepository repository, ILogger<OrderCompletedEventConsumer> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderCompletedEvent> context)
        {
            _logger.LogWarning("We are in consume method now...");
            _logger.LogWarning("BuyerId:" + context.Message.BuyerId);
            return _repository.DeleteCartAsync(context.Message.BuyerId);

        }
    }
}
