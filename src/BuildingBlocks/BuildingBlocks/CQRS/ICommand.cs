using MediatR;
namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// Represents a command in the CQRS pattern that does not return a response.
    /// </summary>
    public interface ICommand : ICommand<Unit>
    {
        
    }
    /// <summary>
    /// Represents a command in the CQRS pattern that returns a response of type <typeparamref name="TResponse"/>.
    /// 
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
