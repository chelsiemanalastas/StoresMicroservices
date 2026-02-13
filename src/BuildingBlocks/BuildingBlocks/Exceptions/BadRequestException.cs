namespace BuildingBlocks.Exceptions;

public class BadRequestException(string details) 
    : Exception($"Details: {details}")
{
}
