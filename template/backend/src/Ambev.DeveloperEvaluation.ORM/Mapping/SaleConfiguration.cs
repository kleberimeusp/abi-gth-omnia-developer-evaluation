using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.Property(s => s.Customer).IsRequired().HasMaxLength(255);
            builder.Property(s => s.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(s => s.Branch).IsRequired().HasMaxLength(100);
            builder.Property(s => s.IsCancelled).HasDefaultValue(false);
            builder.Property(s => s.CreatedAt).HasDefaultValueSql("now()");
            builder.Property(s => s.UpdatedAt).IsRequired(false);
            builder.HasMany(s => s.Items).WithOne().HasForeignKey(i => i.SaleId).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.Property(i => i.Product).IsRequired().HasMaxLength(255);
            builder.Property(i => i.Quantity).IsRequired();
            builder.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(i => i.Discount).HasColumnType("decimal(18,2)");
            builder.Property(i => i.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(i => i.CreatedAt).HasDefaultValueSql("now()");
            builder.Property(i => i.UpdatedAt).IsRequired(false);
            builder.HasOne<Sale>().WithMany(s => s.Items).HasForeignKey(i => i.SaleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
