using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.Events.Handlers
{
    public class SaleCancelledEventHandler : INotificationHandler<SaleCancelledEvent>
    {
        private readonly ILogger<SaleCancelledEventHandler> _logger;

        public SaleCancelledEventHandler(ILogger<SaleCancelledEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Processing SaleCancelledEvent for SaleNumber: {notification.SaleNumber}");

            // TODO: Add additional processing logic here (e.g., updating analytics, sending notifications)

            return Task.CompletedTask;
        }
    }
}
