namespace ShopApi.Entities.DataTransferObjects;

public class ProductForUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Manufacturer { get; set; }
    public IEnumerable<CustomerForCreationDto> Customers { get; set; }
}