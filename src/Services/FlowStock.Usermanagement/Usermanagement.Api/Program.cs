using BuildingBlocks.Application;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using Serilog;
using Usermanagement.Application;
using Usermanagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
.ConfigureResource(resource => resource.AddService("FlowStock.Usermanagement"))
.WithTracing(tracing =>
{
    tracing
    .AddAspNetCoreInstrumentation()
    .AddOtlpExporter(options =>
    {
        options.Endpoint = new Uri("http://localhost:4317");
    });
});

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext();
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastruction(builder.Configuration);
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
