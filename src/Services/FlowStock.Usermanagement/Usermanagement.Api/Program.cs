using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Usermanagement.Application;
using Usermanagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<UsermanagementDbContext>(options=>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("UsermanagementDb"));
});

builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssembly).Assembly);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(ApplicationAssembly).Assembly);
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviors<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();
