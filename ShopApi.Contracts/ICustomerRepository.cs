using ShopApi.Entities.Models;

namespace ShopApi.Contracts;

public interface ICustomerRepository : IRepositoryBase<Customer>
{
    IEnumerable<Customer> GetCustomers(Guid productId, bool trackChanges);
    Customer? GetCustomer(Guid productId, Guid id, bool trackChanges);
    void CreateCustomerForProduct(Guid productId, Customer customer);
    void DeleteCustomer(Customer customer);
}