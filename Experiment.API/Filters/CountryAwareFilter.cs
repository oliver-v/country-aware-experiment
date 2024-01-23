using Experiment.Application.Services;
using Experiment.Application.Services.DK;
using Experiment.Application.Services.EE;
using Experiment.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Experiment.API.Filters;

public class CountryAwareFilter : IActionFilter
{
    private readonly IServiceProvider _serviceProvider;

    public CountryAwareFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ActionArguments.TryGetValue("country", out var countryObject))
        {
            context.Result = new BadRequestObjectResult("Country is required.");
            return;
        }

        if (!Enum.TryParse<Country>(countryObject?.ToString(), out var country))
        {
            context.Result = new BadRequestObjectResult("Country is invalid.");
            return;
        }

        var customerService = GetCustomerService(country);
        context.HttpContext.Items.Add("customerService", customerService);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    private ICustomerService GetCustomerService(Country country)
    {
        switch (country)
        {
            case Country.DK:
                return _serviceProvider.GetRequiredService<IDkCustomerService>();
            case Country.EE:
                return _serviceProvider.GetRequiredService<IEeCustomerService>();
            default:
                throw new ArgumentOutOfRangeException(nameof(country), country, "No matching CustomerService found for the country.");
        }
    }
}