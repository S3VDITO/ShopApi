using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi.Entities.Models;

public class Customer
{
    [Column("CustomerId")] public Guid Id { get; set; }

    [Required(ErrorMessage = "Employee name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Age is a required field.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Address is a required field.")]
    [MaxLength(255, ErrorMessage = "Address length for the Position is 255 characters.")]
    public string Address { get; set; }

    [ForeignKey(nameof(Product))] public Guid ProductId { get; set; }
    public Product Product { get; set; }
}