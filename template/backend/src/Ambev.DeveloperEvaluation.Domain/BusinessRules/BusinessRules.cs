using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Domain.BusinessRules
{
    public static class SaleBusinessRules
    {
        private static readonly ILogger _logger = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        }).CreateLogger("SaleBusinessRules");

        public const int MaxItemsPerSale = 20;
        public const decimal DiscountThresholdLow = 4;
        public const decimal DiscountThresholdHigh = 10;
        public const decimal DiscountLowRate = 0.10m;
        public const decimal DiscountHighRate = 0.20m;

        public static decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity >= DiscountThresholdLow && quantity < DiscountThresholdHigh)
            {
                var discount = unitPrice * quantity * DiscountLowRate;
                _logger.LogInformation($"Applied 10% discount: {discount:C} for {quantity} items.");
                return discount;
            }
            else if (quantity >= DiscountThresholdHigh)
            {
                var discount = unitPrice * quantity * DiscountHighRate;
                _logger.LogInformation($"Applied 20% discount: {discount:C} for {quantity} items.");
                return discount;
            }
            _logger.LogInformation("No discount applied.");
            return 0;
        }

        public static bool ValidateQuantity(int quantity)
        {
            bool isValid = quantity > 0 && quantity <= MaxItemsPerSale;
            _logger.LogInformation($"Quantity validation: {quantity} is {(isValid ? "valid" : "invalid")}.");
            return isValid;
        }
    }
}
