using ECommerice.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerice.Infrastructure.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
           // builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.HasOne(b => b.Category).WithMany()
                    .HasForeignKey(p => p.CategoryId);
            
        }
    }
}