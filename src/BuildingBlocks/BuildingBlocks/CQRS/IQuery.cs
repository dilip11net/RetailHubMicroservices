using MediatR;

namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// Represents a query in the CQRS pattern that returns a response of type <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IQuery<out TResponse>: IRequest<TResponse>
        where TResponse : notnull
    {
    }
}
