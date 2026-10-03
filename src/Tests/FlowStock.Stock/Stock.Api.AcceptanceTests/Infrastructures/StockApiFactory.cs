using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Npgsql;
using Stock.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;

namespace Stock.Api.AcceptanceTests;

public sealed class StockApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        string connectionString = Environment.GetEnvironmentVariable("FLOWSTOCK_TEST_DB")
            ?? throw new InvalidOperationException("FLOWSTOCK_TEST_DB is not exist!");

        NpgsqlConnectionStringBuilder connection = new(connectionString);

        if (connection.Database != "StockAcceptanceTestsDb")
        {
            throw new InvalidOperationException("Acceptance tests must use StockAcceptanceTestsDb!");
        }

        builder.ConfigureServices(services =>
       {
           services.RemoveAll<StockDbContext>();

           services.RemoveAll<DbContextOptions<StockDbContext>>();

           services.RemoveAll<IDbContextOptionsConfiguration<StockDbContext>>();



           services.AddDbContext<StockDbContext>(options =>
           {
               options.UseNpgsql(connectionString);
           });

           services.AddAuthentication(options=>
           {
               options.DefaultAuthenticateScheme = TestAuthHandler.SchemeName;
               options.DefaultChallengeScheme = TestAuthHandler.SchemeName;
           }).AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.SchemeName, _ => {});
       });
    }
}
