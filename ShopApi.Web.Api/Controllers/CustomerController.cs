using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Contracts;
using ShopApi.Entities.DataTransferObjects;
using ShopApi.Entities.Models;

namespace ShopApi.Web.Api.Controllers;

[Route("api/products/{productId:guid}/customers")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public CustomerController(IRepositoryManager repository, ILoggerManager
            logger,
        IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomersForCompany(Guid productId)
    {
        var company = await _repository.Product.GetProductAsync(productId, trackChanges: false);
        if (company == null)
        {
            _logger.LogInfo($"Company with id: {productId} doesn't exist in the database.");
            return NotFound();
        }

        var employeesFromDb = _repository.Customer.GetCustomers(productId,
            trackChanges: false);
        var employeesDto = _mapper.Map<IEnumerable<CustomerDto>>(employeesFromDb);
        return Ok(employeesDto);
    }

    [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")] 
    public async Task<IActionResult> GetCustomerForProduct(Guid productId, Guid id)
    {
        var company = await _repository.Product.GetProductAsync(productId, trackChanges: false);
        if (company == null)
        {
            _logger.LogInfo($"Company with id: {productId} doesn't exist in the database.");
            return NotFound();
        }

        var employeeDb = _repository.Customer.GetCustomer(productId, id,
            trackChanges:
            false);
        if (employeeDb == null)
        {
            _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
            return NotFound();
        }

        var employee = _mapper.Map<CustomerDto>(employeeDb);
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomerForProduct(Guid productId, [FromBody] CustomerForCreationDto customer)
    {
        if (customer == null)
        {
            _logger.LogError("EmployeeForCreationDto object sent from client is null.");
            return BadRequest("EmployeeForCreationDto object is null");
        }
        
        if (!ModelState.IsValid)
        {
            _logger.LogError("Invalid model state for the EmployeeForCreationDto object");
            return UnprocessableEntity(ModelState);
        }

        var company = await _repository.Product.GetProductAsync(productId, trackChanges: false);
        if (company == null)
        {
            _logger.LogInfo($"Company with id: {productId} doesn't exist in the database.");
            return NotFound();
        }

        var employeeEntity = _mapper.Map<Customer>(customer);
        _repository.Customer.CreateCustomerForProduct(productId, employeeEntity);
        await _repository.SaveAsync();
        var employeeToReturn = _mapper.Map<CustomerDto>(employeeEntity);
        return CreatedAtRoute("GetEmployeeForCompany", new
        {
            companyId = productId, id = employeeToReturn.Id
        }, employeeToReturn);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCustomerForProduct(Guid productId, Guid id)
    {
        var company = await _repository.Product.GetProductAsync(productId, trackChanges: false);
        if (company == null)
        {
            _logger.LogInfo($"Company with id: {productId} doesn't exist in the database.");
            return NotFound();
        }
        var employeeForCompany = _repository.Customer.GetCustomer(productId, id,
            trackChanges: false);
        if (employeeForCompany == null)
        {
            _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
            return NotFound();
        }
        _repository.Customer.DeleteCustomer(employeeForCompany);
        await _repository.SaveAsync();
        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCustomerForProduct(Guid productId, Guid id, [FromBody]
        CustomerForUpdateDto customer)
    {
        if (customer == null)
        {
            _logger.LogError("EmployeeForUpdateDto object sent from client is null.");
            return BadRequest("EmployeeForUpdateDto object is null");
        }
        
        if (!ModelState.IsValid)
        {
            _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
            return UnprocessableEntity(ModelState);
        }
        
        var company = await _repository.Product.GetProductAsync(productId, trackChanges: false);
        if (company == null)
        {
            _logger.LogInfo($"Company with id: {productId} doesn't exist in the database.");
            return NotFound();
        }
        var employeeEntity = _repository.Customer.GetCustomer(productId, id,
            trackChanges:
            true);
        if (employeeEntity == null)
        {
            _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
            return NotFound();
        }
        _mapper.Map(customer, employeeEntity);
        await _repository.SaveAsync();
        return NoContent();
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PartiallyUpdateCustomerForProduct(Guid productId, Guid id,
        [FromBody] JsonPatchDocument<CustomerForUpdateDto> patchDoc)
    {
        if (patchDoc == null)
        {
            _logger.LogError("patchDoc object sent from client is null.");
            return BadRequest("patchDoc object is null");
        }
        var company = await _repository.Product.GetProductAsync(productId, trackChanges: false);
        if (company == null)
        {
            _logger.LogInfo($"Company with id: {productId} doesn't exist in the database.");
            return NotFound();
        }
        var customerEntity = _repository.Customer.GetCustomer(productId, id,
            trackChanges:
            true);
        if (customerEntity == null)
        {
            _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
            return NotFound();
        }
        var customerToPatch = _mapper.Map<CustomerForUpdateDto>(customerEntity);
        patchDoc.ApplyTo(customerToPatch, ModelState);
        TryValidateModel(customerToPatch);
        if(!ModelState.IsValid)
        {
            _logger.LogError("Invalid model state for the patch document");
            return UnprocessableEntity(ModelState);
        }
        _mapper.Map(customerToPatch, customerEntity);
        await _repository.SaveAsync();
        return NoContent();
    }
}