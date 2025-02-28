using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ambev.DeveloperEvaluation.Domain.BusinessRules;

namespace Ambev.DeveloperEvaluation.ORM.Entities
{
    public class SaleItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        private int _quantity;

        [Required]
        [Range(1, 20, ErrorMessage = "Maximum limit is 20 items per product.")]
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

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal Discount { get; private set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [NotMapped]
        public decimal CalculatedTotal => (UnitPrice * Quantity) - Discount;

        public event Action<SaleItem> ItemCancelled;

        public void OnItemCancelled()
        {
            ItemCancelled?.Invoke(this);
        }

        private void ApplyDiscount()
        {
            Discount = SaleBusinessRules.CalculateDiscount(Quantity, UnitPrice);
        }
    }
}
