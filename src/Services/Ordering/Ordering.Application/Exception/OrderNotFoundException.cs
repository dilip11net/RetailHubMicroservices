using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exception
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid orderId) : base("Order", orderId)
        {
        }
    }
}
