using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        private readonly ILogger<Sale> _logger;
        private readonly IBus _bus;

        public Sale(ILogger<Sale> logger, IBus bus)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
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

        public List<SaleItem> Items { get; set; } = new();

        public bool IsCancelled { get; set; } = false;

        public event Action<Sale>? SaleCreated;
        public event Action<Sale>? SaleModified;
        public event Action<Sale>? SaleCancelled;

        public async Task OnSaleCreated()
        {
            _logger.LogInformation($"Sale Created: {SaleNumber}");
            var saleEvent = new SaleCreatedEvent(
                Id, SaleNumber, SaleDate, Customer, TotalAmount, Branch, IsCancelled
            );
            await _bus.Publish(saleEvent);
            SaleCreated?.Invoke(this);
        }

        public async Task OnSaleModified()
        {
            _logger.LogInformation($"Sale Modified: {SaleNumber}");
            var saleEvent = new SaleModifiedEvent(
                Id, SaleNumber, SaleDate, Customer, TotalAmount, Branch, IsCancelled
            );
            await _bus.Publish(saleEvent);
            SaleModified?.Invoke(this);
        }

        public async Task OnSaleCancelled()
        {
            _logger.LogInformation($"Sale Cancelled: {SaleNumber}");
            var saleEvent = new SaleCancelledEvent(
                Id, SaleNumber, SaleDate, Customer, TotalAmount, Branch, IsCancelled
            );
            await _bus.Publish(saleEvent);
            SaleCancelled?.Invoke(this);
        }
    }
}
