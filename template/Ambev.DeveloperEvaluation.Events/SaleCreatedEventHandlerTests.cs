using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Events.Handlers;
using Bogus;
using NSubstitute;
using Xunit;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Events
{
    public class SaleCreatedEventHandlerTests
    {
        private readonly ILogger<SaleCreatedEventHandler> _logger;
        private readonly SaleCreatedEventHandler _handler;

        public SaleCreatedEventHandlerTests()
        {
            _logger = Substitute.For<ILogger<SaleCreatedEventHandler>>();
            _handler = new SaleCreatedEventHandler(_logger);
        }

        [Fact]
        public async Task Handle_Should_LogInformation_When_EventIsHandled()
        {
            // Arrange
            var faker = new Faker();
            var saleEvent = new SaleCreatedEvent(
                Guid.NewGuid(),
                faker.Commerce.Ean13(),
                faker.Date.Recent(),
                faker.Person.FullName,
                faker.Finance.Amount(10, 1000),
                faker.Company.CompanyName(),
                false
            );

            // Act
            await _handler.Handle(saleEvent, CancellationToken.None);

            // Assert
            _logger.Received(1).LogInformation($"Processing SaleCreatedEvent for SaleNumber: {saleEvent.SaleNumber}");
        }
    }
}
