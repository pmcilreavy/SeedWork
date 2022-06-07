namespace SeedWork.Command;

public interface ICommandHandler<in TCommand, TResult> where TCommand : IDomainCommand<TResult>
{
    Task<TResult> Handle(TCommand command, CancellationToken cancellationToken = default);
}