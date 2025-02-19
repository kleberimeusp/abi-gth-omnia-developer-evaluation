using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.ORM.Entities
{
    public class Sale
    {
        private readonly ILogger<Sale> _logger;

        public Sale(ILogger<Sale> logger)
        {
            _logger = logger;
        }

        [Key]
        public int Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; }
        public bool IsCancelled { get; set; }

        public List<SaleItem> Items { get; set; } = new List<SaleItem>();

        public event Action<Sale> SaleCreated;
        public event Action<Sale> SaleModified;
        public event Action<Sale> SaleCancelled;

        public void OnSaleCreated()
        {
            _logger.LogInformation($"Sale Created: {SaleNumber}");
            SaleCreated?.Invoke(this);
        }

        public void OnSaleModified()
        {
            _logger.LogInformation($"Sale Modified: {SaleNumber}");
            SaleModified?.Invoke(this);
        }

        public void OnSaleCancelled()
        {
            _logger.LogInformation($"Sale Cancelled: {SaleNumber}");
            SaleCancelled?.Invoke(this);
        }
    }

}
