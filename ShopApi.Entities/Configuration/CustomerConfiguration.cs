using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApi.Entities.Models;

namespace ShopApi.Entities.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasData(new Customer
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                Name = "Sam Raiden",
                Age = 26,
                Address = "Software developer",
                ProductId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new Customer
            {
                Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                Name = "Jana McLeaf",
                Age = 30,
                Address = "Software developer",
                ProductId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new Customer
            {
                Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                Name = "Kane Miller",
                Age = 35,
                Address = "Administrator",
                ProductId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
            });
    }
}