using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ambev.DeveloperEvaluation.Domain.BusinessRules;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.ORM.Entities
{
    public class SaleItem
    {
        [Key]
        public int Id { get; set; }
        public int SaleId { get; set; }
        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }
        public string ProductName { get; set; }
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value > 20)
                    throw new InvalidOperationException("Cannot sell more than 20 identical items");
                _quantity = value;
                ApplyDiscount();
            }
        }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; private set; }
        public decimal TotalPrice { get; set; }

        [NotMapped]
        public decimal CalculatedTotal => (UnitPrice * Quantity) - Discount;

        public event Action<SaleItem> ItemCancelled;

        public void OnItemCancelled()
        {
            if (Sale != null)
            {
                var logger = (ILogger<SaleItem>)Sale.GetType().GetProperty("_logger")?.GetValue(Sale);
                logger?.LogInformation($"Item Cancelled: {ProductName} in Sale {Sale.SaleNumber}");
            }
            ItemCancelled?.Invoke(this);
        }

        private void ApplyDiscount()
        {
            Discount = BusinessRules.CalculateDiscount(Quantity, UnitPrice);
        }
    }
}
