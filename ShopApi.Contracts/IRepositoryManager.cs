namespace ShopApi.Contracts;

public interface IRepositoryManager
{
    IProductRepository Product { get; }
    ICustomerRepository Customer { get; }
    Task SaveAsync();
}