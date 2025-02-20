namespace Ambev.DeveloperEvaluation.ORM.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Rebus.Bus;

    namespace Ambev.DeveloperEvaluation.Domain.Entities
    {
        public class Sale : IRequest<Sale>
        {
            private readonly ILogger<Sale> _logger;
            private readonly IBus _bus;

            public Sale(ILogger<Sale> logger, IBus bus)
            {
                _logger = logger;
                _bus = bus;
            }

            [Key]
            public Guid Id { get; set; }

            [Required]
            public string SaleNumber { get; set; }

            [Required]
            public DateTime SaleDate { get; set; }

            [Required]
            public string Customer { get; set; }

            [Required]
            public decimal TotalAmount { get; set; }

            [Required]
            public string Branch { get; set; }

            public List<SaleItem> Items { get; set; } = new List<SaleItem>();

            public bool IsCancelled { get; set; } = false;

            public event Action<Sale> SaleCreated;
            public event Action<Sale> SaleModified;
            public event Action<Sale> SaleCancelled;

            public async void OnSaleCreated()
            {
                _logger.LogInformation($"Sale Created: {SaleNumber}");
                await _bus.Publish(new SaleCreatedEvent(this));
                SaleCreated?.Invoke(this);
            }

            public async void OnSaleModified()
            {
                _logger.LogInformation($"Sale Modified: {SaleNumber}");
                await _bus.Publish(new SaleModifiedEvent(this));
                SaleModified?.Invoke(this);
            }

            public async void OnSaleCancelled()
            {
                _logger.LogInformation($"Sale Cancelled: {SaleNumber}");
                await _bus.Publish(new SaleCancelledEvent(this));
                SaleCancelled?.Invoke(this);
            }
        }

    }