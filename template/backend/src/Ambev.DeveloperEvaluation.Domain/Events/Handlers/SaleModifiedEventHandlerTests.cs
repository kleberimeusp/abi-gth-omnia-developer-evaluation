using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Events.Handlers;
using Bogus;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Tests.Events
{
    public class SaleModifiedEventHandlerTests
    {
        private readonly ILogger<SaleModifiedEventHandler> _logger;
        private readonly SaleModifiedEventHandler _handler;

        public SaleModifiedEventHandlerTests()
        {
            _logger = Substitute.For<ILogger<SaleModifiedEventHandler>>();
            _handler = new SaleModifiedEventHandler(_logger);
        }

        [Fact]
        public async Task Handle_Should_LogInformation_When_EventIsHandled()
        {
            // Arrange
            var faker = new Faker();
            var saleEvent = new SaleModifiedEvent(
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
            _logger.Received(1).LogInformation($"Processing SaleModifiedEvent for SaleNumber: {saleEvent.SaleNumber}");
        }
    }
}
