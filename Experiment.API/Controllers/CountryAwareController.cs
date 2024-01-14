using Experiment.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Experiment.API.Controllers;

public class CountryAwareController : ControllerBase
{
    protected ICustomerService CustomerService =>
        HttpContext.Items["customerService"] as ICustomerService 
        ?? throw new ArgumentException("CustomerService not found");
}