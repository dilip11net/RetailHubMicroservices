using MediatR;
namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// Represents a handler for a command in the CQRS pattern that does not return a response.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<in TCommand> 
        : ICommandHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {
    }

    /// <summary>
    /// Represents a handler for a command in the CQRS pattern that returns a response of type <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface ICommandHandler<in TCommand, TResponse> : MediatR.IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
    }
}
