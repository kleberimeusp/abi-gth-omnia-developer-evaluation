using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary> 
/// Represents a sale transaction in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets the sale date.
    /// </summary>
    public DateTime Date { get; set; } 

    /// <summary>
    /// Gets the customer associated with the sale.
    /// </summary>
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Gets the total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets the branch where the sale was made.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the sale was cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets the items sold in the transaction.
    /// </summary>
    public List<SaleItem> Items { get; set; } = new List<SaleItem>();

    /// <summary>
    /// Gets the date and time when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the sale information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the Sale class.
    /// </summary>
    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Cancels the sale transaction.
    /// Changes the sale status to cancelled.
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
