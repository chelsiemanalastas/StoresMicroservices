namespace Ordering.Application.Exceptions;

public class OrderNotFoundException(string action, string orderName, string orderId) 
    : Exception($"{action} failed. Order {orderName} with id {orderId} was not found.")
{

}
