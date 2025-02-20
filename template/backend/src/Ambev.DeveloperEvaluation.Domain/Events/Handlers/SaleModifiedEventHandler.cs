using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events.Handlers
{
    public class SaleModifiedEventHandler : INotificationHandler<SaleModifiedEvent>
    {
        private readonly ILogger<SaleModifiedEventHandler> _logger;

        public SaleModifiedEventHandler(ILogger<SaleModifiedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Processing SaleModifiedEvent for SaleNumber: {notification.SaleNumber}");

            // TODO: Add additional processing logic here (e.g., updating analytics, sending notifications)

            return Task.CompletedTask;
        }
    }
}
