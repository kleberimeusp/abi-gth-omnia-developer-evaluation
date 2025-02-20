using Ambev.DeveloperEvaluation.Domain.DTOs;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Commands.Sales
{
    public class CreateSaleCommand : IRequest<SaleDto>
    {
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; }
        public List<SaleItemDto> Items { get; set; }
    }
}
