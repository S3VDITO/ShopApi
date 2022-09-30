using ShopApi.Contracts;
using ShopApi.Entities;
using ShopApi.Entities.Models;

namespace ShopApi.Repository;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }
    
    public IEnumerable<Customer> GetCustomers(Guid productId, bool trackChanges) =>
        FindByCondition(e => e.ProductId.Equals(productId), trackChanges)
            .OrderBy(e => e.Name);
    
    public Customer? GetCustomer(Guid productId, Guid id, bool trackChanges) =>
        FindByCondition(e => e.ProductId.Equals(productId) && e.Id.Equals(id),
            trackChanges).SingleOrDefault();
    
    public void CreateCustomerForProduct(Guid productId, Customer customer)
    {
        customer.ProductId = productId;
        Create(customer);
    }
    
    public void DeleteCustomer(Customer customer) => Delete(customer);
}