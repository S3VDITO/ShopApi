using Microsoft.EntityFrameworkCore;
using ShopApi.Contracts;
using ShopApi.Entities;
using ShopApi.Entities.Models;

namespace ShopApi.Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges)
        => await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();
    public async Task<Product> GetProductAsync(Guid productId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(productId), trackChanges)
            .SingleOrDefaultAsync();
    public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<Guid> ids, bool
        trackChanges) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();
    
    public void CreateCompany(Product product) => Create(product);
    
    public IEnumerable<Product> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
        FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();
    
    public void DeleteCompany(Product product) => Delete(product);
}