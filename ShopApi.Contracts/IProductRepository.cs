using ShopApi.Entities.Models;

namespace ShopApi.Contracts;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges);
    Task<Product> GetProductAsync(Guid productId, bool trackChanges);
    void CreateCompany(Product product);
    Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<Guid> ids, bool
        trackChanges);
    void DeleteCompany(Product product);
}