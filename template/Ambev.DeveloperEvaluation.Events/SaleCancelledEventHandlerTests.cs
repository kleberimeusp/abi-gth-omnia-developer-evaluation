using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Events.Handlers;
using Bogus;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Tests.Events
{
    public class SaleCancelledEventHandlerTests
    {
        private readonly ILogger<SaleCancelledEventHandler> _logger;
        private readonly SaleCancelledEventHandler _handler;

        public SaleCancelledEventHandlerTests()
        {
            _logger = Substitute.For<ILogger<SaleCancelledEventHandler>>();
            _handler = new SaleCancelledEventHandler(_logger);
        }

        [Fact]
        public async Task Handle_Should_LogInformation_When_EventIsHandled()
        {
            // Arrange
            var faker = new Faker();
            var saleEvent = new SaleCancelledEvent(
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
            _logger.Received(1).LogInformation($"Processing SaleCancelledEvent for SaleNumber: {saleEvent.SaleNumber}");
        }
    }
}
