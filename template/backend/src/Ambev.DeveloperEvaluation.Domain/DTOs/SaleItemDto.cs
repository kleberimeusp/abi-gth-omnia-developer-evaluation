﻿namespace Ambev.DeveloperEvaluation.Domain.DTOs
{
    public class SaleItemDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
