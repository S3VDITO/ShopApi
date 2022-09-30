using ShopApi.Contracts;
using ShopApi.Entities;

namespace ShopApi.Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private IProductRepository _productRepository;
    private ICustomerRepository _customerRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public IProductRepository Product
    {
        get
        {
            if (_productRepository == null)
                _productRepository = new ProductRepository(_repositoryContext);
            return _productRepository;
        }
    }

    public ICustomerRepository Customer
    {
        get
        {
            if (_customerRepository == null)
                _customerRepository = new CustomerRepository(_repositoryContext);
            return _customerRepository;
        }
    }

    public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
}