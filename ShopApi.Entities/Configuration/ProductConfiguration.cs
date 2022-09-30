using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApi.Entities.Models;

namespace ShopApi.Entities.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData
        (
            new Product
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                Name = "IT_Solutions Ltd",
                Description = "583 Wall Dr. Gwynn Oak, MD 21207",
                Manufacturer = "USA"
            },
            new Product
            {
                Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                Name = "Admin_Solutions Ltd",
                Description = "312 Forest Avenue, BF 923",
                Manufacturer = "USA"
            }
        );
    }
}