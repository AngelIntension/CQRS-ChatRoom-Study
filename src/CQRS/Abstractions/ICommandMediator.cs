namespace CQRS.Abstractions
{
    public interface ICommandMediator
    {
        void Register<TCommand>(ICommandHandler<TCommand> commandHandler)
            where TCommand : ICommand;
        void Send<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}