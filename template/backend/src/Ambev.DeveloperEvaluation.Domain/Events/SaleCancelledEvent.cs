using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public record SaleCancelledEvent(
        Guid Id,
        string SaleNumber,
        DateTime SaleDate,
        string Customer,
        decimal TotalAmount,
        string Branch,
        bool IsCancelled
    ) : INotification
    {
        // Método FromSale para converter um objeto Sale em um SaleCreatedEvent
        public static SaleCancelledEvent FromSale(Sale sale) =>
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
