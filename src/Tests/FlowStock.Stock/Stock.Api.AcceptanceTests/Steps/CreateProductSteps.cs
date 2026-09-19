using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;
using Stock.Infrastructure;

namespace Stock.Api.AcceptanceTests;

[Binding]
public sealed class CreateProductSteps
{
    private StockApiFactory? _factory;
    private HttpClient? _client;

    private Guid _categoryId;

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

        _client = _factory.CreateClient();
    }

    [AfterScenario]
    public async Task DisposeTestEnvironment()
    {
        _factory?.Dispose();
        _client?.Dispose();
    }

    [Given("an active product category exists")]
    public Task AnActiveProductCategoryExists()
    {
        throw new PendingStepException();
    }

    [Given("the current user has permission to create products")]
    public Task TheCurrentUserHasPermissionToCreateProducts()
    {
        throw new PendingStepException();
    }

    [When("the user creates a product in that category")]
    public Task WhenTheUserCreatesAProductInThatCategory()
    {
        throw new PendingStepException();
    }

    [Then("the product should be created successfully")]
    public void ThenTheProductShouldBeCreatedSuccessfully()
    {
        throw new PendingStepException();
    }

    [Then("the created product identifier should be returned")]
    public Task ThenTheCreatedProductIdentifierShouldBeReturned()
    {
        throw new PendingStepException();
    }

    [Given("the selected product category does not exist")]
    public void GivenTheSelectedProductCategoryDoesNotExist()
    {
        throw new PendingStepException();
    }

    [Then("product creation should be rejected")]
    public void ThenProductCreationShouldBeRejected()
    {
        throw new PendingStepException();
    }

    [Then("no product should be stored")]
    public Task ThenNoProductShouldBeStored()
    {
        throw new PendingStepException();
    }

    [Given("products with the following codes already exist")]
    public Task GivenProductsWithTheFollowingCodesAlreadyExist(
        DataTable table)
    {
        throw new PendingStepException();
    }

    [When("a new product is created")]
    public Task WhenANewProductIsCreated()
    {
        throw new PendingStepException();
    }

    [Then("a code should be assigned to the new product")]
    public Task ThenACodeShouldBeAssignedToTheNewProduct()
    {
        throw new PendingStepException();
    }

    [Then("the generated code should not match any existing product code")]
    public Task ThenTheGeneratedCodeShouldNotMatchAnyExistingProductCode()
    {
        throw new PendingStepException();
    }
}
