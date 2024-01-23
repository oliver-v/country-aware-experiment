using Experiment.API.Models;
using Experiment.Application.Models;
using Experiment.Domain;
using Experiment.Domain.Entities;
using Experiment.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Experiment.API.Controllers;

[ApiController]
[Route("/api/v1/customers")]
public class CustomerController : CountryAwareController
{
    private readonly IUnitOfWork unitOfWork;
    
    public CustomerController(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Country country, CreateCustomerRequestDto request)
    {
        var cust = new Customer(); // <-- This is the Customer entity from the Domain layer and it's not supposed to be used here

        var model = new CustomerModel
        {
            Name = request.Name,
            IdCode = request.IdCode,
            Gender = request.Gender
        };

        var createdCustomer = await CustomerService.CreateCustomerAsync(model);

        return Created("/api/v1/customers", createdCustomer);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(Country country, string idCode)
    {
        var customer = await unitOfWork.Customers
            .GetAll(x => x.Country == country && x.IdCode == idCode)
            // .Include(x => x.Contacts)
            .SingleOrDefaultAsync();

        return Ok(customer);
    }
}