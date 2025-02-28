using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public record SaleCreatedEvent(
        Guid Id,
        string SaleNumber,
        DateTime SaleDate,
        string Customer,
        decimal TotalAmount,
        string Branch,
        bool IsCancelled
    ) : INotification
    {
        public static SaleCreatedEvent FromSale(Sale sale) =>
            new(
                sale.Id,
                sale.SaleNumber,
                sale.SaleDate,
                sale.Customer,
                sale.TotalAmount,
                sale.Branch,
                sale.IsCancelled
            );
    }
}
