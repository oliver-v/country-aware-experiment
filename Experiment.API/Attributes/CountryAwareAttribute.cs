// using Experiment.Application.Enums;
// using Experiment.Application.Factories;
// using Experiment.Application.Services;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
//
// namespace Experiment.API.Attributes;
//
// public class CountryAwareAttribute : ActionFilterAttribute
// {
//     private readonly IServiceProvider _serviceProvider;
//
//     public CountryAwareAttribute(IServiceProvider serviceProvider)
//     {
//         _serviceProvider = serviceProvider;
//     }
//     
//     public override void OnActionExecuting(ActionExecutingContext context)
//     {
//         if (!context.ActionArguments.TryGetValue("country", out var countryObject))
//         {
//             context.Result = new BadRequestObjectResult("Country is required.");
//             return;
//         }
//         
//         if (!Enum.TryParse<Country>(countryObject?.ToString(), out var country))
//         {
//             context.Result = new BadRequestObjectResult("Country is invalid.");
//             return;
//         }
//        
//         var customerService = CustomerServiceFactory.CreateCustomerService(country);
//         context.HttpContext.Items.Add("customerService", customerService);
//
//         base.OnActionExecuting(context);
//     }
//     
//     private ICustomerService GetCustomerService(Country country)
//     {
//         switch (country)
//         {
//             case Country.Denmark:
//                 return _serviceProvider.GetRequiredService<DkCustomerService>();
//             // Add cases for other countries and corresponding services
//             default:
//                 throw new ArgumentOutOfRangeException(nameof(country), country, "No matching CustomerService found for the country.");
//         }
//     }
// }