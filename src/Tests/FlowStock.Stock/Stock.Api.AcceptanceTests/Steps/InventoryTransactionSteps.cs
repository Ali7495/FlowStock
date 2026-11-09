using Reqnroll;

namespace Stock.Api.AcceptanceTests;

[Binding]
public class InventoryTransactionSteps
{
    [Given("the user has permission to manage inventory")]
    public void GivenTheUserHasPermissionToManageInventory()
    {
        throw new NotImplementedException();
    }

    [Given("an active product exists with {double} units in stock")]
    public void GivenAnActiveProductExistsWithUnitsInStock(double initialStock)
    {
        throw new NotImplementedException();
    }

    [When("the user adds {double} units to the inventory")]
    public void WhenTheUserAddsUnitsToTheInventory(double quantity)
    {
        throw new NotImplementedException();
    }

    [When("the user deducts {double} units from the inventory")]
    public void WhenTheUserDeductsUnitsFromTheInventory(double quantity)
    {
        throw new NotImplementedException();
    }

    [Then("the inventory transaction should be created successfully")]
    public void ThenTheInventoryTransactionShouldBeCreatedSuccessfully()
    {
        throw new NotImplementedException();
    }

    [Then("the product stock should be {double} units")]
    public void ThenTheProductStockShouldBeUnits(double expectedStock)
    {
        throw new NotImplementedException();
    }

    [Then("the transaction should have type {string} and quantity {double}")]
    public void ThenTheTransactionShouldHaveTypeAndQuantity(
        string transactionType,
        double quantity)
    {
        throw new NotImplementedException();
    }

    [Then("the inventory transaction should not be created")]
    public void ThenTheInventoryTransactionShouldNotBeCreated()
    {
        throw new NotImplementedException();
    }

    [Then("the product's stock remains with {double} units")]
    public void ThenTheProductStockRemainsWithUnits(double expectedStock)
    {
        throw new NotImplementedException();
    }
}
