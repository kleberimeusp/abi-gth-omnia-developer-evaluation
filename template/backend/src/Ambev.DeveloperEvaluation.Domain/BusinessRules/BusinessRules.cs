namespace Ambev.DeveloperEvaluation.Domain.BusinessRules
{
    public static class BusinessRules
    {
        public static decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items");
            else if (quantity >= 10)
                return unitPrice * quantity * 0.2m;
            else if (quantity >= 4)
                return unitPrice * quantity * 0.1m;
            else
                return 0;
        }
    }
}
