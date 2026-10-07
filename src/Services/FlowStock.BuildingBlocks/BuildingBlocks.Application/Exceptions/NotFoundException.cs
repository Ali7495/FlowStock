namespace BuildingBlocks.Application;

public class NotFoundException : Exception
{
    public NotFoundException(string resourceName, object id)
            : base($"{resourceName} with id '{id}' was not found.")
    {
    }
}
