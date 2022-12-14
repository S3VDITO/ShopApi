using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi.Entities.Models;

public class Product
{
    [Column("ProductId")] public Guid Id { get; set; }

    [Required(ErrorMessage = "Company name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Company address is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for rhe Address is 60 characte")]
    public string Description { get; set; }

    public string Manufacturer { get; set; }
    public ICollection<Customer> Customers { get; set; }
}