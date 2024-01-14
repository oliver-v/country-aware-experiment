using Experiment.API.Models;
using Experiment.Application.Enums;
using Experiment.Application.Models;
using Experiment.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Experiment.API.Controllers;

[ApiController]
[Route("/api/v1/customers")]
public class CustomerController() : CountryAwareController
{
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
}