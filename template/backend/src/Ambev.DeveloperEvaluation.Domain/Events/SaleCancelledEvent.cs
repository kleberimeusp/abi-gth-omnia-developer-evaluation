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
    ) : INotification;
}
