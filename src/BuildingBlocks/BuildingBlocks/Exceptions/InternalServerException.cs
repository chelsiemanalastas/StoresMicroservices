namespace BuildingBlocks.Exceptions;

public class InternalServerException(string action, string details) 
    : Exception($"Failed to execute {action}. Details: {details}")
{

}
