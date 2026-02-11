namespace Catalog.Domain.Exceptions;

public class NotFoundException(string resourceType, string filterField, string resourceIdentifier)
    : Exception($"{resourceType} with {filterField} of '{resourceIdentifier}' was not found.")
{
}
