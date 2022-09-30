using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Contracts;
using ShopApi.Entities.DataTransferObjects;
using ShopApi.Entities.Models;
using ShopApi.Web.Api.ModelBinders;

namespace ShopApi.Web.Api.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public ProductsController(IRepositoryManager repository, ILoggerManager
        logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _repository.Product.GetAllProductsAsync(trackChanges:
            false);
        var companiesDto = _mapper.Map<IEnumerable<ProductDto>>(products);
        return Ok(companiesDto);
    }

    [HttpGet("{id:guid}", Name = "ProductById")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _repository.Product.GetProductAsync(id, trackChanges:
            false);
        if (product == null)
        {
            _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
            return NotFound();
        }
        else
        {
            var companyDto = _mapper.Map<ProductDto>(product);
            return Ok(companyDto);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationDto
        product)
    {
        if (product == null)
        {
            _logger.LogError("ProductForCreationDto object sent from client is null.");
            return BadRequest("ProductForCreationDto object is null");
        }

        var productEntity = _mapper.Map<Product>(product);
        _repository.Product.CreateCompany(productEntity);
        await _repository.SaveAsync();
        var companyToReturn = _mapper.Map<ProductDto>(productEntity);
        return CreatedAtRoute("ProductById", new { id = companyToReturn.Id },
            companyToReturn);
    }

    [HttpGet("collection/({ids})", Name = "ProductCollection")]
    public async Task<IActionResult> GetProductCollection(
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        if (ids == null)
        {
            _logger.LogError("Parameter ids is null");
            return BadRequest("Parameter ids is null");
        }

        var productEntities = await _repository.Product.GetByIdsAsync(ids,
            trackChanges: false);
        if (ids.Count() != productEntities.Count())
        {
            _logger.LogError("Some ids are not valid in a collection");
            return NotFound();
        }

        var productToReturn =
            _mapper.Map<IEnumerable<ProductDto>>(productEntities);
        return Ok(productToReturn);
    }

    [HttpPost("collection")]
    public async Task<IActionResult> CreateProductCollection(
        [FromBody] IEnumerable<ProductForCreationDto> companyCollection)
    {
        if (companyCollection == null)
        {
            _logger.LogError("Product collection sent from client is null.");
            return BadRequest("Product collection is null");
        }

        var companyEntities = _mapper.Map<IEnumerable<Product>>(companyCollection);
        foreach (var company in companyEntities)
        {
            _repository.Product.CreateCompany(company);
        }

        await _repository.SaveAsync();
        var productCollectionToReturn =
            _mapper.Map<IEnumerable<ProductDto>>(companyEntities);
        var ids = string.Join(",", productCollectionToReturn.Select(c => c.Id));
        return CreatedAtRoute("CompanyCollection", new { ids },
            productCollectionToReturn);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody]
        ProductForUpdateDto product)
    {
        if (product == null)
        {
            _logger.LogError("CompanyForUpdateDto object sent from client is null.");
            return BadRequest("CompanyForUpdateDto object is null");
        }
        var productEntity = await _repository.Product.GetProductAsync(id,
            trackChanges:
            true);
        if (productEntity == null)
        {
            _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
            return NotFound();
        }
        _mapper.Map(product, productEntity);
        await _repository.SaveAsync();
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var product = await _repository.Product.GetProductAsync(id, trackChanges:
            false);
        if (product == null)
        {
            _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
            return NotFound();
        }
        _repository.Product.DeleteCompany(product);
        await _repository.SaveAsync();
        return NoContent();
    }
}