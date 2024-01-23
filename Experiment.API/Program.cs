using System.Text.Json;
using System.Text.Json.Serialization;
using Experiment.API.Filters;
using Experiment.Application.Services.DK;
using Experiment.Application.Services.EE;
using Experiment.Infrastructure;
using Experiment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CustomerDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IDkCustomerService, DkCustomerService>();
builder.Services.AddScoped<IEeCustomerService, EeCustomerService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddControllers(options =>
    {
        options.Filters.Add<CountryAwareFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });
        

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); 

app.Run();