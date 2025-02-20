using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events.Handlers
{
    public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
    {
        private readonly ILogger<SaleCreatedEventHandler> _logger;

        public SaleCreatedEventHandler(ILogger<SaleCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Processing SaleCreatedEvent for SaleNumber: {notification.SaleNumber}");

            // TODO: Add additional processing logic here (e.g., updating analytics, sending notifications)

            return Task.CompletedTask;
        }
    }
}
