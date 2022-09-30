namespace ShopApi.Entities.DataTransferObjects;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
}