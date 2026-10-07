using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Stock.Application;
using Stock.Domain;
using Stock.Infrastructure;

namespace Stock.Api.AcceptanceTests;

[Binding]
public sealed class CreateProductSteps
{
    private StockApiFactory? _factory = null;
    private HttpClient? _client = null;
    private HttpResponseMessage _response = null;

    private Guid _categoryId;
    private Guid _createdProductId;
    private string _productName = null;

    [BeforeScenario]
    public async Task PrepareTestEnvironment()
    {
        _factory = new StockApiFactory();

        using IServiceScope scope = _factory.Services.CreateScope();

        StockDbContext dbContext = scope.ServiceProvider.GetRequiredService<StockDbContext>();

        if (dbContext.Database.GetDbConnection().Database != "StockAcceptanceTestsDb")
        {
            throw new InvalidOperationException("Unexpected database for acceptance tests!");
        }

        await dbContext.Database.MigrateAsync();

        await dbContext.Set<Product>().IgnoreQueryFilters().ExecuteDeleteAsync();
        await dbContext.Set<ProductCategory>().IgnoreQueryFilters().ExecuteDeleteAsync();

        await dbContext.Database.ExecuteSqlRawAsync("ALTER SEQUENCE product_code_seq RESTART WITH 1");

        _client = _factory.CreateClient();
    }

    [AfterScenario]
    public void DisposeTestEnvironment()
    {
        _factory?.Dispose();
        _client?.Dispose();
        _response?.Dispose();
    }

    [Given("an active product category exists")]
    public async Task AnActiveProductCategoryExists()
    {
        ProductCategory productCategory = ProductCategory.Create($"Acceptance Category {Guid.NewGuid():N}");

        using IServiceScope serviceScope = _factory.Services.CreateScope();

        StockDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<StockDbContext>();

        await dbContext.Set<ProductCategory>().AddAsync(productCategory);

        await dbContext.SaveChangesAsync();

        _categoryId = productCategory.Id;
    }

    [Given("the current user has permission to create products")]
    public Task TheCurrentUserHasPermissionToCreateProducts()
    {
        _client?.DefaultRequestHeaders.Remove("X-Test-UserId");
        _client?.DefaultRequestHeaders.Remove("X-Test-Permission");

        _client?.DefaultRequestHeaders.Add("X-Test-UserId", Guid.NewGuid().ToString());
        _client?.DefaultRequestHeaders.Add("X-Test-Permission", Permissions.ProductCreate);

        return Task.CompletedTask;

    }

    [When("the user creates a product in that category")]
    public async Task WhenTheUserCreatesAProductInThatCategory()
    {
        _productName = $"Acceptance Product {Guid.NewGuid():N}";

        ProductCommand productCommand = new(_categoryId, _productName);

        _response = await _client.PostAsJsonAsync("/api/Product", productCommand);

        if (_response.IsSuccessStatusCode)
        {
            Guid? id = await _response.Content.ReadFromJsonAsync<Guid>();

            _createdProductId = id.GetValueOrDefault();
        }
    }

    [Then("the product should be created successfully")]
    public async Task ThenTheProductShouldBeCreatedSuccessfully()
    {
        string responseBody =
            await _response.Content.ReadAsStringAsync();

        _response.StatusCode.Should().Be(
            System.Net.HttpStatusCode.Created,
            $"API response was: {responseBody}");
    }

    [Then("the created product identifier should be returned")]
    public async Task ThenTheCreatedProductIdentifierShouldBeReturned()
    {
        _createdProductId.Should().NotBeEmpty();

        using IServiceScope scope = _factory.Services.CreateScope();

        StockDbContext dbContext = scope.ServiceProvider.GetRequiredService<StockDbContext>();

        bool isProductExist = await dbContext.Set<Product>().AnyAsync(p => p.Id == _createdProductId && p.ProductCategoryId == _categoryId && p.Name == _productName);

        isProductExist.Should().BeTrue();
    }

    [Given("the selected product category does not exist")]
    public void GivenTheSelectedProductCategoryDoesNotExist()
    {
        _categoryId = Guid.NewGuid();
    }

    [Then("product creation should be rejected")]
    public void ThenProductCreationShouldBeRejected()
    {
        _response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Then("no product should be stored")]
    public async Task ThenNoProductShouldBeStored()
    {
        using IServiceScope scope = _factory.Services.CreateScope();

        StockDbContext dbContext = scope.ServiceProvider.GetRequiredService<StockDbContext>();

        bool isProductExist = await dbContext.Set<Product>().AnyAsync(p => p.ProductCategoryId == _categoryId && p.Name == _productName);

        isProductExist.Should().BeFalse();
    }


}
